using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.models;
using GuessingGame.Repositories;

namespace GuessingGame.Services;

public class LobbyService: ILobbyService
{

    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;

    public LobbyService(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }
    
    public LobbyDTO CreateRoomAndPlayer(string playerDisplayName)
    {
        // TODO: Make transactions work again. Inject connection, not con string?
        // _dbHelper.StartTransaction();
        // Generate lobby code
        var code = Guid.NewGuid().ToString().ToUpper().Substring(0, 6); // Generate game code
        var room = new Room()
        {
            Code = code
        };
        // Add room to db
        room = _roomRepository.Create(room);
    
        // Create player, add to db
        Player player = CreatePlayer(playerDisplayName);
        room = _roomRepository.AddPlayer(room, player);
        
        // Associate player with room Because
        // we retrieved player before adding room to db
        player.Room = room;

        // map to dto & return
        return _mapper.Map<LobbyDTO>(player);

    }

    public LobbyDTO JoinLobby(string roomCode, string playerDisplayName)
    {
        // Get room by code
        var room = _roomRepository.GetByCode(roomCode);
        if (room == null)
        {
            throw new Exception("Room not found");
        }
        // Create player & add to db
        Player player = CreatePlayer(playerDisplayName);
        room = _roomRepository.AddPlayer(room, player);
        
        // Associate player with room Because
        // we retrieved player before adding room to db
        player.Room = room;
        
        // map to dto & return
        return _mapper.Map<LobbyDTO>(player);
    }
    
    // Create player & add to db
    private Player CreatePlayer(string displayName)
    {
        var playerSessionId = Guid.NewGuid().ToString(); // generate session ID
        var player = new Player()
        {
            Session = playerSessionId,
            DisplayName = displayName,
        };
        return player;
    }
}