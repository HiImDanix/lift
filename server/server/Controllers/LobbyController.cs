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
        // join the group
        _gameHubContext.Groups.AddToGroupAsync(lobby.Id.ToString(), lobby.Room.Id.ToString());
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
        _gameHubContext.Clients.All.PlayerJoined(newPlayer);
        // Join the group
        _gameHubContext.Groups.AddToGroupAsync(newPlayer.Id.ToString(), lobby.Room.Id.ToString());
    return Ok(lobby);
    }
}