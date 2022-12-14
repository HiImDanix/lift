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
    [Authorize("Administrator")]
    // TODO: auth policies
    public async Task<IActionResult> AnswerQuestion([FromRoute] int gameId, AnswerQuestionRequest request)
    {

        var playerIdString = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        var playerId = int.Parse(playerIdString);
        var questionAnsweredDto = _guessingGameService.AnswerQuestion(playerId, request.GameQuestionId, request.Answer);
        
        if (questionAnsweredDto == null)
        {
            throw new Exception("Could not submit the answer");
        }
        
        await _gameHubContext.Clients.Group(gameId.ToString()).QuestionAnswered(questionAnsweredDto);
        
        return Ok();
    }
}