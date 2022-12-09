using System.Security.Cryptography;
using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.hubs;
using GuessingGame.hubs.Clients;
using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace GuessingGame.Services;

public class GuessingGameService: IGuessingGameService
{
    // Todo: Coupling SignalR with the service is not ideal. Find a better way to do this.
    private readonly IHubContext<GameHub, IGameClient> _gameHubContext;
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;
    
    public GuessingGameService(IHubContext<GameHub, IGameClient> gameHubContext, IMapper mapper, IQuestionRepository questionRepository)
    {
        _gameHubContext = gameHubContext;
        _mapper = mapper;
        _questionRepository = questionRepository;
    }

    // TODO: Use repository pattern to store the game state
    // Start game loop
    public void StartGame(GuessingGameModel model)
    {
        // Start game loop in a new thread
        Task.Run(() => GameLoop(model));
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
            // Get random question
            model.CurrentQuestion = GetRandomQuestion();
            // Scramble the answers
            model.CurrentQuestion.Answers = model.CurrentQuestion.Answers.OrderBy(x => Guid.NewGuid()).ToList();
            // Print answers
            foreach (var answer in model.CurrentQuestion.Answers)
            {
                Console.WriteLine(answer.AnswerText);
            }

            // Send round start DTO
            var roundStartDto = _mapper.Map<RoundStartDto>(model);
            _gameHubContext.Clients.Group(model.Room.Id.ToString()).RoundStarted(roundStartDto);
            
            // Wait for round to end
            Thread.Sleep(model.RoundDurationMs);
            
            // Send round end DTO. If last round, send game end DTO
            if (round == model.TotalRounds)
            {
                model.Status = GameStatus.Finished.ToString();
                _gameHubContext.Clients.Group(model.Room.Id.ToString()).GameFinished();
            } else
            {
                model.Status = GameStatus.Scoreboard.ToString();
                _gameHubContext.Clients.Group(model.Room.Id.ToString()).RoundFinished();
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