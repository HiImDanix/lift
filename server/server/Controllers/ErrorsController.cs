using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers;

public class ErrorsController : ControllerBase
{
    [NonAction]
    [Route("error")]
    public IActionResult Error()
    {
        System.Console.WriteLine("Error");
        return Problem();
    }
}