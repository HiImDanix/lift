using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.hubs;
using GuessingGame.hubs.Clients;
using GuessingGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GuessingGame.Controllers;

[ApiController]
public class LobbyController: ControllerBase
{

    private readonly ILobbyService _lobbyService;
    private readonly IHubContext<GameHub, IGameClient> _gameHubContext;

    public LobbyController(ILobbyService lobbyService, IHubContext<GameHub, IGameClient> gameHubContext)
    {
        _lobbyService = lobbyService;
        _gameHubContext = gameHubContext;
    }

    [HttpPost]
    [Route("lobby")]
    public IActionResult CreateLobby(CreateRoomRequest request)
    {
        var lobby = _lobbyService.CreateRoomAndPlayer(playerDisplayName: request.PlayerName);
        return Ok(lobby);
    }
    
    // Join room by code
    [HttpPost]
    [Route("lobby/{roomCode}/join")]
    public IActionResult JoinLobby(string roomCode, JoinRoomRequest request)
    {
        var lobby = _lobbyService.JoinLobby(roomCode, request.PlayerName);
        var newPlayer = new PlayerPublicDTO()
        {
            Id = lobby.Id,
            Name = lobby.Name
        };
        // Notify all players in the lobby that a new player has joined
        _gameHubContext.Clients.Group(lobby.Room.Id.ToString()).PlayerJoined(newPlayer);
        return Ok(lobby);
    }
    
    // Retrieve lobby info by ID
    [HttpGet]
    [Route("lobby/{lobbyId}")]
    public IActionResult GetLobby(int lobbyId)
    {
        var lobby = _lobbyService.GetLobby(lobbyId);
        return Ok(lobby);
    }
    
    // Start game
    [HttpPost]
    [Route("lobby/{lobbyId}/start")]
    public IActionResult StartGame(int lobbyId)
    {
        _lobbyService.StartGame(lobbyId);
        // Notify all players in the lobby that the game has started
        _gameHubContext.Clients.Group(lobbyId.ToString()).GameStarted();
        return Ok();
    }
}