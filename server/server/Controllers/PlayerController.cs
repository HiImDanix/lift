using GuessingGame.dto;
using GuessingGame.models;
using GuessingGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayersController: ControllerBase
{
    
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    
    [HttpPost]
    public IActionResult CreatePlayer(CreatePlayerRequest request)
    {
        var player = new Player(
            Guid.NewGuid().ToString(),
            request.DisplayName
        );
        
        // TODO: Save player to database
        PrivatePlayerResponse response = _playerService.CreatePlayer(player);
        
        return CreatedAtAction(
            nameof(GetPlayer),
            new {id = player.Id},
            response
        );
    }
    
    [HttpGet()]
    public IActionResult GetPlayers(CreatePlayerRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetPlayer(int id)
    {
        PrivatePlayerResponse? response = _playerService.GetPlayer(id);
        return response == null ? NotFound() : Ok(response);
    }
}