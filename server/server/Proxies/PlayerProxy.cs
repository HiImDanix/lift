using GuessingGame.models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class PlayerProxy: Player
{
    private readonly IRoomRepository _roomRepository;
    
    public PlayerProxy(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    
    public override Room? Room
    {
        get
        {
            if (base.Room == null)
            {
                base.Room = _roomRepository.GetRoomById(base.Id);
            }
            
            return base.Room;
        }
        
        set => base.Room = value;
    }
}