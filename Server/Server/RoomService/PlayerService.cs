using GuessingGame.dto;
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

    public PrivatePlayerResponse CreatePlayer(Player player)
    {
    var createdPlayer = _playerRepository.Create(player);

    return new PrivatePlayerResponse(
            ID: createdPlayer.PlayerId,
            Session: createdPlayer.Session,
            DisplayName: createdPlayer.DisplayName
        );
    }

    public PrivatePlayerResponse? GetPlayer(int id)
    {
        var player = _playerRepository.Get(id);

        if (player == null)
        {
            return null;
        }

        return new PrivatePlayerResponse(
            ID: player.PlayerId,
            Session: player.Session,
            DisplayName: player.DisplayName
        );
    }
}