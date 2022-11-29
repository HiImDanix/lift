using GuessingGame.DTO.requests;
using GuessingGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers;

[ApiController]
public class LobbyController: ControllerBase
{

    private readonly ILobbyService _lobbyService;

    public LobbyController(ILobbyService lobbyService)
    {
        _lobbyService = lobbyService;
    }
    
    [HttpPost]
    [Route("lobby")]
    public IActionResult CreateLobby(CreateRoomRequest request)
    {

        var room = _lobbyService.CreateRoomAndPlayer(playerDisplayName: request.PlayerName);
        return Ok(room);
    }
    
    // Join room by code
    [HttpPost]
    [Route("lobby/{roomCode}/join")]
    public IActionResult JoinLobby(string roomCode, JoinRoomRequest request)
    {
        var room = _lobbyService.JoinLobby(roomCode, request.PlayerName);
        return Ok(room);
    }
}