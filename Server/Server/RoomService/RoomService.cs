using GuessingGame.models;
using GuessingGame.Repositories;

namespace GuessingGame.Services;

public class RoomService: IRoomService
{

    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
            _roomRepository = roomRepository;
    }


    public Room CreateRoom(string playerDisplayName)
    {
        // TODO: Transaction
        
        // Room
        var code = Guid.NewGuid().ToString().ToUpper().Substring(0, 4); // Generate game code
        var room = new Room(code);
        _roomRepository.Create(room);
        
        // Player (save and associate to room)
        var playerSessionId = Guid.NewGuid().ToString(); // generate session ID
        var player = new Player(session:playerSessionId, displayName:playerDisplayName);
        _roomRepository.AddPlayer(room, player);
        
        // TODO: Return DTO
        return room;

    }
}