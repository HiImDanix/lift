using AutoMapper;
using Moq;
using FluentAssertions;
using GuessingGame.DTO;
using GuessingGame.DTO.responses;
using GuessingGame.Models;
using GuessingGame.Repositories;
using GuessingGame.Services;

namespace tests;

public class QuestionServiceTests
{
    
    private readonly IMapper _mapper;
    
    public QuestionServiceTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
        _mapper = config.CreateMapper();
    }
    
    [Fact]
    public void CreateQuestionWithAnswers_ShouldAddQuestionAndAnswersToDatabase()
    {
        // ===== Arrange =====
        string imageUrl = "http://www.example.com/image.jpg";
        string questionText = "What is the capital of France?";
        string category = "Geography";
        List<Answer> answers = new List<Answer> {
            new() { AnswerText = "Paris", IsCorrect = true},
            new() { AnswerText = "London", IsCorrect = false},
            new() { AnswerText = "Berlin", IsCorrect = false},
            new() { AnswerText = "Rome", IsCorrect = false}
        };
        // Question object
        Question question = new Question {
            ImagePath = imageUrl,
            QuestionText = questionText,
            Category = category,
            Answers = answers
        };

        // Mock repositories & their methods
        Mock<IQuestionRepository> questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.Add(It.IsAny<Question>())).Returns(question);
        Mock<IAnswerRepository> answerRepositoryMock = new Mock<IAnswerRepository>();
        answerRepositoryMock.Setup(x => x.Add(It.IsAny<Answer>())).Returns((Answer ans) => ans);

        // Create service
        QuestionService questionService = new QuestionService(questionRepositoryMock.Object, answerRepositoryMock.Object, _mapper);

        // ===== Act =====
        var result = questionService.CreateQuestionWithAnswers(imageUrl, questionText, category, answers);
        
        // ===== Assert =====
        // Expected question DTO
        QuestionDTO questionDto = _mapper.Map<QuestionDTO>(question);
        // Expected response
        result.Should().BeOfType<QuestionDTO>();
        result.Should().BeEquivalentTo(questionDto); // structurally equal (same properties)
        // Verify that the repositories were called
        questionRepositoryMock.Verify(x => x.Add(It.IsAny<Question>()), Times.Once);
        answerRepositoryMock.Verify(x => x.Add(It.IsAny<Answer>()), Times.Exactly(answers.Count));
    }
}

