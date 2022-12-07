﻿using GuessingGame.DTO.requests;
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
        public IActionResult CreateQuestion([FromForm] QuestionCreateRequest request)
        {
            var answersList = request.Answers.Select(x => new Answer()
            {
                AnswerText = x.AnswerText,
                IsCorrect = x.IsCorrect
            }).ToList();
            
            var question = _questionService.CreateQuestionWithAnswers(
                request.ImagePath,
                request.QuestionText,
                request.Category,
                answersList
            );
            return Ok(question);
        }
        
        [HttpGet]
        [Route("questions")]
        public IActionResult GetQuestions()
        {
            var questions = _questionService.GetQuestions();
            return Ok(questions);
        }
    }
}
