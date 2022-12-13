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

    [HttpPost("games/{gameId}/answers")]
    [Authorize]
    // TODO: auth policies
    public async Task<IActionResult> AnswerQuestion([FromRoute] int gameId, AnswerQuestionRequest request)
    {
        var playerIdString = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        var playerId = int.Parse(playerIdString);
        var result = _guessingGameService.AnswerQuestion(playerId, request.GameQuestionId, request.Answer);
        
        if (result == null)
        {
            return NotFound();
        }
        
        await _gameHubContext.Clients.Group(gameId.ToString()).QuestionAnswered(result);
        
        return Ok();
    }
}