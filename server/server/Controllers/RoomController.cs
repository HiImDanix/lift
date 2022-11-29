using GuessingGame.DTO.requests;
using GuessingGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController: ControllerBase
{

    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    [HttpPost]
    public IActionResult CreateRoom(CreateRoomRequest request)
    {

        var room = _roomService.CreateRoom(playerDisplayName: request.PlayerName);
        return Ok(room);
    }
}