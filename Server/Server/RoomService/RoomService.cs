using GuessingGame.models;
using GuessingGame.Repositories;

namespace GuessingGame.Services;

public class RoomService: IRoomService
{

    private readonly IRoomRepository _roomRepository;
    private readonly IDbHelper _dbHelper;

    public RoomService(IRoomRepository roomRepository, IDbHelper dbHelper)
    {
        _roomRepository = roomRepository;
        _dbHelper = dbHelper;
    }


    public Room CreateRoom(string playerDisplayName)
    {
        // TODO: Transaction
        _dbHelper.StartTransaction();
        Room room;
        try
        {
            var code = Guid.NewGuid().ToString().ToUpper().Substring(0, 4); // Generate game code
            room = new Room(code);
            _roomRepository.Create(room);
        
            // Player (save and associate to room)
            var playerSessionId = Guid.NewGuid().ToString(); // generate session ID
            var player = new Player(session:playerSessionId, displayName:playerDisplayName);
            _roomRepository.AddPlayer(room, player);
        }
        catch (Exception e)
        {
            _dbHelper.RollbackTransaction();
            // TODO: Throw custom exception
            throw;
        }
        
        // TODO: Return DTO
        return room;

    }
}