using GuessingGame.DTO.requests;
using GuessingGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers
{
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        [Route("questions")]
        public IActionResult CreateQuestion(string imagePath, string questionText, string category, string answer)
        {
            var question = _questionService.CreateQuestion(imagePath, questionText, category, answer);
            return Ok(question);
        }
    }
}
