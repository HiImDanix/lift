using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class RoomProxy: Room
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGuessingGameRepository _guessingGameRepository;

    public RoomProxy(IPlayerRepository playerRepository, IGuessingGameRepository guessingGameRepository)
    {
        _playerRepository = playerRepository;
        _guessingGameRepository = guessingGameRepository;
    }

    public override IList<Player>? Players
    {
        get
        {
            if (base.Players == null)
            {
                base.Players = _playerRepository.GetPlayersByRoomId(Id);
            }

            return base.Players;
        }
        
        set => base.Players = value;
    }

    public override Player? Host
    {
        get
        {
            if (base.Host == null)
            {
                base.Host = _playerRepository.GetHostByRoomId(Id);
            }

            return base.Host;
        }
    }
    
    public override GuessingGameModel CurrentGame
    {
        get
        {
            if (base.CurrentGame == null)
            {
                base.CurrentGame = _guessingGameRepository.GetByRoomId(Id);
            }
            

            return base.CurrentGame;
        }
    }
}