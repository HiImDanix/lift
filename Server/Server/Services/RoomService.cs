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
        // TODO: Make transactions work again. Inject connection, not con string?
        // _dbHelper.StartTransaction();
        var code = Guid.NewGuid().ToString().ToUpper().Substring(0, 6); // Generate game code
        var room = new Room()
        {
            Code = code
        };
        room = _roomRepository.Create(room);
    
        // Player (save and associate to room)
        var playerSessionId = Guid.NewGuid().ToString(); // generate session ID
        var player = new Player()
        {
            Session = playerSessionId,
            DisplayName = playerDisplayName,
        };
        room = _roomRepository.AddPlayer(room, player);

            // TODO: Return DTO
            return room;

    }
}