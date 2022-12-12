using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.hubs;
using GuessingGame.hubs.Clients;
using GuessingGame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GuessingGame.Controllers;

[ApiController]
public class QuizController: ControllerBase
{

    private readonly IGuessingGameService _guessingGameService;
    private readonly IHubContext<GameHub, IGameClient> _gameHubContext;

    public QuizController(IHubContext<GameHub, IGameClient> gameHubContext, IGuessingGameService guessingGameService)
    {
        _gameHubContext = gameHubContext;
        _guessingGameService = guessingGameService;
    }

    // [HttpPost("game/{gameId}/question/{questionId}/answer")]
    // [Authorize]
    // public async Task<IActionResult> AnswerQuestion([FromRoute] Guid gameId, [FromRoute] Guid questionId, [FromForm] string answer)
    // {
    //     var PlayerID = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
    //     
    //     var result = await _guessingGameService.AnswerQuestion(PlayerID, gameId, questionId, answer);
    //     
    //     if (result == null)
    //     {
    //         return NotFound();
    //     }
    //     
    //     await _gameHubContext.Clients.Group(gameId.ToString()).QuestionAnswered(result);
    //     
    //     return Ok();
    // }
}