using GuessingGame.models;
using GuessingGame.Repositories;

namespace GuessingGame.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }


}