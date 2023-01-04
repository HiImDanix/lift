using System.Security.Cryptography;
using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.hubs;
using GuessingGame.hubs.Clients;
using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace GuessingGame.Services.Games;

public class GuessingGameService: IGuessingGameService
{
    // Todo: Coupling SignalR with the service is not ideal. Find a better way to do this.
    private readonly IHubContext<GameHub, IGameClient> _gameHubContext;
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;
    private readonly IGuessingGameRepository _guessingGameRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IPlayerAnswersRepository _playerAnswersRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IScoreboardRepository _scoreboardRepository;
    
    public GuessingGameService(IHubContext<GameHub, IGameClient> gameHubContext, IMapper mapper,
        IQuestionRepository questionRepository, IGuessingGameRepository guessingGameRepository, IRoomRepository roomRepository,
        IPlayerAnswersRepository playerAnswersRepository, IPlayerRepository playerRepository, IScoreboardRepository scoreboardRepository)
    {
        _gameHubContext = gameHubContext;
        _mapper = mapper;
        _questionRepository = questionRepository;
        _guessingGameRepository = guessingGameRepository;
        _roomRepository = roomRepository;
        _playerAnswersRepository = playerAnswersRepository;
        _playerRepository = playerRepository;
        _scoreboardRepository = scoreboardRepository;
    }
    
    // Start game loop
    public GuessingGameModel StartGame(GuessingGameModel model)
    {
        model = _guessingGameRepository.Add(model); // No need to inform player as done in lobby controller, start game method.
        var room = model.Room;
        // Set as current game for room
         _roomRepository.UpdateCurrentGame(room, model);
        
        // Start game loop in a new thread
        Task.Run(() => GameLoop(model));
        return model;
    }

    // Game loop
    private void GameLoop(GuessingGameModel game)
    {
        // Players have already been notified that the game will start and are shown instructions page, so we wait.
        Thread.Sleep((int)(game.StartTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
        
        // Play the rounds
        for (int round = 1; round <= game.TotalRounds; round++)
        {
            // Prepare game for the round
            game.Status = GameStatus.Playing.ToString();
            game.CurrentRound = round;
            game.CurrentRoundStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            // Add to db
            _guessingGameRepository.Update(game);
            // Add random question to DB. We have to add it to DB so we can get the ID and set the current question.
            var question = GetRandomQuestion();
            var questionForGame = new QuizGameQuestion()
            {
                Question = question,
                Game = game
            };
            var questionForGameInDb = _guessingGameRepository.Add(questionForGame);
            // Mark the question as current question for the game
            game.CurrentQuizGameQuestion = questionForGameInDb;
            // Update the game in the DB
            _guessingGameRepository.Update(game);
            // Scramble the answers TODO: put this in automapper
            game.CurrentQuizGameQuestion.Question.Answers = game.CurrentQuizGameQuestion.Question.Answers.OrderBy(x => Guid.NewGuid()).ToList();

            // Send round start DTO
            var roundStartDto = _mapper.Map<RoundStartDto>(game);
            _gameHubContext.Clients.Group(game.Room.Id.ToString()).RoundStarted(roundStartDto);
            
            // Wait for round to end
            Thread.Sleep(game.RoundDurationMs);
            
            // Send round end DTO. If last round, send game end DTO. This will show the scoreboard.
            if (round == game.TotalRounds)
            {
                game.Status = GameStatus.Finished.ToString();
                // Save to db
                _guessingGameRepository.Update(game);
                // Create scoreboard
                var scoreboardDto = UpdateScoreboard(game);
                // Inform players
                _gameHubContext.Clients.Group(game.Room.Id.ToString()).GameFinished(scoreboardDto);
            } else
            {
                game.Status = GameStatus.Scoreboard.ToString();
                // Save to db
                _guessingGameRepository.Update(game);
                // Create scoreboard
                var scoreboardDto = UpdateScoreboard(game);
                // Inform players
                _gameHubContext.Clients.Group(game.Room.Id.ToString()).RoundFinished(scoreboardDto);
                // Wait for players to look over the scoreboard
                Thread.Sleep(game.ScoreboardDurationMs);
            }
        }
    }

    private ScoreboardDTO UpdateScoreboard(GuessingGameModel game)
    {
        
        var answers = game.CurrentQuizGameQuestion.Answers;
        foreach(var ans in answers)
        {
            var player = _playerRepository.Get(ans.Player.Id);
            // Calculate score
            var score = ans.Answer.IsCorrect ? 100 : 0;
            // Add to scoreboard DTO or update existing
            var existingScore = _scoreboardRepository.GetScoreForPlayer(game, player);
            if (existingScore == null)
            {
                _scoreboardRepository.add(game, player, score);
            } else
            {
                _scoreboardRepository.UpdateScoreById(existingScore.Id, existingScore.Score + score);
            }
        }
        
        var scoreboardLines = _scoreboardRepository.GetScoreboardByGuessingGameId(game.Id);
        // return & wrap scoreboard in object as otherwise mapping will not work.
        var scoreboard = new Scoreboard{Scores = scoreboardLines};

        return _mapper.Map<ScoreboardDTO>(scoreboard);

    }

    private Question GetRandomQuestion()
    {
        var questions = _questionRepository.GetAll();
        var random = new Random();
        var randomIndex = random.Next(0, questions.Count);
        return questions[randomIndex];
    }
    
    // TODO: Custom exceptions
    // Note: not thread safe. E.g. If player clicks fast enough, they can submit multiple answers. 
    public QuestionAnsweredDTO AnswerQuestion(int playerId, int quizGameQuestionID, string answer)
    {
        // Get player
        var player = _playerRepository.Get(playerId);
        // Get game
        var quizGameQuestion = _questionRepository.getQuizGameQuestion(quizGameQuestionID);
        
        // Validate
        if (player == null || quizGameQuestion == null)
        {
            throw new Exception("Player or question not found");
        }
        
        // Check if player can answer for this game
        if (player.Room.Id != quizGameQuestion.Game.Room.Id)
        {
            throw new Exception("You are not in the same room as this game");
        }

        // Check if game is in playing state
        if (quizGameQuestion.Game.Status != GameStatus.Playing.ToString())
        {
            throw new Exception("Game is not in playing state");
        }
        
        // Check if question is current question
        if (quizGameQuestion.Game.CurrentQuizGameQuestion?.Id != quizGameQuestion.Id)
        {
            throw new Exception("Question is not the current question");
        }
        
        // Check if player has already answered
        if (_playerAnswersRepository.IsAnswered(player, quizGameQuestion))
        {
            throw new Exception("You have already answered this question");
        }

        // Get answer object from text
        var answerObject = quizGameQuestion.Question.Answers.FirstOrDefault(a => a.AnswerText == answer);
        if (answerObject == null)
        {
            throw new Exception("Invalid answer");
        }
        
        // Save answer to DB
        _playerAnswersRepository.Add(new PlayerAnswer()
        {
            Player = player,
            Answer = answerObject,
            QuizGameQuestion = quizGameQuestion,
            AnsweredTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        });
        
        // Return DTO
        var dto = new QuestionAnsweredDTO()
        {
            player = _mapper.Map<PlayerPublicDTO>(player),
            answerId = answerObject.Id
        };
        return dto;

    }
}