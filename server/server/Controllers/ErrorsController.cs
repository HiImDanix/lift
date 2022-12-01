using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers;
[Route("/error")]
public class ErrorsController : Controller
{
    [NonAction]
    [AllowAnonymous]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context.Error;
        var result = new { error = exception.GetType().Name, message = exception.Message };
        return new JsonResult(result);
    }
    
    [NonAction]
    [AllowAnonymous]
    public IActionResult Error(int statusCode)
    {
        var result = new { error = statusCode };
        return new JsonResult(result);
    }
}