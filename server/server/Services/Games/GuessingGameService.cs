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
    
    public GuessingGameService(IHubContext<GameHub, IGameClient> gameHubContext, IMapper mapper,
        IQuestionRepository questionRepository, IGuessingGameRepository guessingGameRepository, IRoomRepository roomRepository)
    {
        _gameHubContext = gameHubContext;
        _mapper = mapper;
        _questionRepository = questionRepository;
        _guessingGameRepository = guessingGameRepository;
        _roomRepository = roomRepository;
    }
    
    // Start game loop
    public GuessingGameModel StartGame(GuessingGameModel model)
    {
        model = _guessingGameRepository.Add(model); // No need to inform player as done in lobby controller, start game method.
        var room = model.Room;
        // Set as current game for room
         _roomRepository.updateCurrentGame(room, model);
        
        // Start game loop in a new thread
        Task.Run(() => GameLoop(model));
        return model;
    }

    // Game loop
    private void GameLoop(GuessingGameModel model)
    {
        // Players have already been notified that the game will start and are shown instructions page, so we wait.
        Thread.Sleep((int)(model.StartTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
        
        // Play the rounds
        for (int round = 1; round <= model.TotalRounds; round++)
        {
            // Prepare model for the round
            model.Status = GameStatus.Playing.ToString();
            model.CurrentRound = round;
            model.CurrentRoundStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            model.CurrentQuestion = GetRandomQuestion();
            // Add to db
            _guessingGameRepository.Update(model);
            // Scramble the answers TODO: put this in automapper
            model.CurrentQuestion.Answers = model.CurrentQuestion.Answers.OrderBy(x => Guid.NewGuid()).ToList();

            // Send round start DTO
            var roundStartDto = _mapper.Map<RoundStartDto>(model);
            _gameHubContext.Clients.Group(model.Room.Id.ToString()).RoundStarted(roundStartDto);
            
            // Wait for round to end
            Thread.Sleep(model.RoundDurationMs);
            
            // Send round end DTO. If last round, send game end DTO. This will show the scoreboard.
            if (round == model.TotalRounds)
            {
                model.Status = GameStatus.Finished.ToString();
                // Save to db
                _guessingGameRepository.Update(model);
                // Inform players
                _gameHubContext.Clients.Group(model.Room.Id.ToString()).GameFinished();
            } else
            {
                model.Status = GameStatus.Scoreboard.ToString();
                // Save to db
                _guessingGameRepository.Update(model);
                // Inform players
                _gameHubContext.Clients.Group(model.Room.Id.ToString()).RoundFinished();
                // Wait for players to look over the scoreboard
                Thread.Sleep(model.ScoreboardDurationMs);
            }
        }
    }

    private Question GetRandomQuestion()
    {
        var questions = _questionRepository.GetAll();
        var random = new Random();
        var randomIndex = random.Next(0, questions.Count);
        return questions[randomIndex];
    }
}