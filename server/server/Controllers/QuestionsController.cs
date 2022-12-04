using GuessingGame.DTO.requests;
using GuessingGame.Models;
using GuessingGame.Services;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult CreateQuestion(QuestionCreateRequest request)
        {
            var answers = request.Answers.Select(x => new Answer()
            {
                AnswerText = x.Answer,
                IsCorrect = x.IsCorrect
            }).ToList();
            
            var question = _questionService.CreateQuestionWithAnswers(
                request.ImagePath,
                request.QuestionText,
                request.Category,
                answers
            );
            return Ok(question);
        }
    }
}
