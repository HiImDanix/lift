using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("Greetings")]
public class ExampleController : ControllerBase
{

    private readonly ILogger<ExampleController> _logger;

    public ExampleController(ILogger<ExampleController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "getHelloWorld")]
    public string Get()
    {
        return "Hello world!";
    }
    
    [HttpGet("{name}", Name = "getHelloName")]
    public string Get(string name)
    {
        return $"Hello {name}!";
    }

}