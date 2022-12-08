using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.hubs;
using GuessingGame.hubs.Clients;
using GuessingGame.models;
using GuessingGame.Models;
using Microsoft.AspNetCore.SignalR;

namespace GuessingGame.Services;

public class GuessingGameService: IGuessingGameService
{
    // Todo: Coupling SignalR with the service is not ideal. Find a better way to do this.
    private readonly IHubContext<GameHub, IGameClient> _gameHubContext;
    private readonly IMapper _mapper;
    
    public GuessingGameService(IHubContext<GameHub, IGameClient> gameHubContext, IMapper mapper)
    {
        _gameHubContext = gameHubContext;
        _mapper = mapper;
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
        Thread.Sleep((int)(model.startTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
        for (int round = 1; round <= model.totalRounds; round++)
        {
            model.Status = GameStatus.Playing.ToString();
            model.currentRound = round;
            model.currentRoundStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Console.WriteLine($"Round {round} started");
            // Send round start DTO
            var roundStartDto = _mapper.Map<RoundStartDto>(model);
            _gameHubContext.Clients.Group(model.room.Id.ToString()).RoundStarted(roundStartDto);
            // Wait for round to end
            Thread.Sleep(model.roundDurationMs);
            if (round == model.totalRounds)
            {
                model.Status = GameStatus.Finished.ToString();
                _gameHubContext.Clients.Group(model.room.Id.ToString()).GameFinished();
            } else
            {
                model.Status = GameStatus.Scoreboard.ToString();
                _gameHubContext.Clients.Group(model.room.Id.ToString()).RoundFinished();
                Thread.Sleep(model.scoreboardDurationMs);
            }
        }
    }
}