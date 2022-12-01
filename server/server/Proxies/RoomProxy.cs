using GuessingGame.models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class RoomProxy: Room
{
    private readonly IPlayerRepository _playerRepository;

    public RoomProxy(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
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
}