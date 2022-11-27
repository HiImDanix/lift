using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.models;
using GuessingGame.Repositories;

namespace GuessingGame.Services;

public class RoomService: IRoomService
{

    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;

    public RoomService(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }
    
    public PlayerPrivateWithRoomDTO CreateRoom(string playerDisplayName)
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
        player.Room = room;

        // map to dto & return
        return _mapper.Map<PlayerPrivateWithRoomDTO>(player);

    }
}