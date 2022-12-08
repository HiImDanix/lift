using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.models;
using GuessingGame.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace GuessingGame.Services;

public class LobbyService: ILobbyService
{

    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;
    private readonly IJWTService _jwtService;

    public LobbyService(IRoomRepository roomRepository, IMapper mapper, IJWTService jwtService)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public LobbyDTO CreateRoomAndPlayer(string playerDisplayName)
    {
        // TODO: Make transactions work again. Inject connection, not con string?
        // _dbHelper.StartTransaction();
        // Generate lobby code
        var code = Guid.NewGuid().ToString().ToUpper().Substring(0, 6); // Generate game code
        var room = new Room()
        {
            Code = code,
        };
        // Add room to db
        room = _roomRepository.Create(room);
    
        // Create player, add to db
        Player player = CreatePlayer(playerDisplayName);
        player = _roomRepository.AddPlayer(room, player);

        // Set host
        room = _roomRepository.SetHost(room, player);

        // Set room because it was retrieved before the player was added to db
        player.Room = room;

        var token = _jwtService.GenerateJwtToken(player);
        player.Session = token;

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
        player = _roomRepository.AddPlayer(room, player);

        // Associate player with room Because
        // we retrieved player before adding room to db
        player.Room = room;

        // TODO: Do not use session for this
        var token = _jwtService.GenerateJwtToken(player);
        player.Session = token;
        
        // map to dto & return
        return _mapper.Map<LobbyDTO>(player);
    }

    public RoomDTO GetLobby(int lobbyId)
    {
        var lobby = _roomRepository.Get(lobbyId);
        return _mapper.Map<RoomDTO>(lobby);
    }

    public RoomDTO StartGame(int lobbyId)
    {
        var lobby = _roomRepository.Get(lobbyId);
        if (lobby == null)
        {
            throw new Exception("Lobby not found");
        }
        // Save time as epoch
        long startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 3000;
        var room = _roomRepository.UpdateStartTime(lobby, startTime);
        return _mapper.Map<RoomDTO>(room);
    }

    // Create player & add to db
    private Player CreatePlayer(string displayName)
    {
        var player = new Player()
        {
            // TODO: Remove! not needed anymore.
            Session = Guid.NewGuid().ToString(),
            DisplayName = displayName,
        };
        return player;
    }
}