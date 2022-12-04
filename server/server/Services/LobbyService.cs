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
    private readonly IConfiguration _configuration;

    public LobbyService(IRoomRepository roomRepository, IMapper mapper, IConfiguration configuration)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
        _configuration = configuration;
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
        
        // JWT
        var token = GenerateJwtToken(player);
        
        // Associate player with room Because
        // we retrieved player before adding room to db
        player.Room = room;
        // TODO: Do not use session for this
        player.Session = token;

        // map to dto & return
        return _mapper.Map<LobbyDTO>(player);

    }

    private string GenerateJwtToken(Player player)
    {
        // JWT
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // claims
        var claims = new List<Claim>
        {
            // id
            new("sub", player.Id.ToString()),
            new("name", player.DisplayName)
        };
        
        var token = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
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
        var player = new Player()
        {
            // TODO: Remove! not needed anymore.
            Session = new Guid().ToString(),
            DisplayName = displayName,
        };
        return player;
    }
}