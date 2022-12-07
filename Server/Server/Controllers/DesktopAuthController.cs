using GuessingGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers;

[ApiController]
public class DesktopAuthController : ControllerBase
{
    private readonly IDesktopAuthService _desktopAuthService;
    public DesktopAuthController(IDesktopAuthService desktopAuthService)
    {
        _desktopAuthService = desktopAuthService;
    }
    [HttpPost]
    [Route("login")]
    public IActionResult Index([FromForm] string email, [FromForm] string password)
    {
        var loggedIn = _desktopAuthService.Login(email, password);
        if(loggedIn)
        {
            return Ok();
        }
        return NotFound();
    }
}
