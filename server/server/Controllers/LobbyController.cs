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
    [Route("players")]
    public IActionResult CreateLobby(CreateRoomRequest request)
    {

        var room = _lobbyService.CreateRoomAndPlayer(playerDisplayName: request.PlayerName);
        return Ok(room);
    }
}