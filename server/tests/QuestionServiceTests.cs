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
        var config = new MapperConfiguration(
            cfg => cfg.AddProfile(new AutoMapperProfile()));
        _mapper = config.CreateMapper();
    }
    
    [Theory]
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography",
        new[] { "Paris", "London", "Berlin", "Rome" })]
    [InlineData("", "What is the capital of France?", "Geography",
        new[] { "Paris", "London", "Berlin", "Rome" })]
    public void CreateQuestionWithAnswers_ShouldAddQuestionAndAnswersToDatabase(
        string imageUrl, string questionText, string category,
        string[] answers)
    {
        // ===== Arrange =====
        // Create question with answers
        List<Answer> answerObjects = answers.Select(a => new Answer { AnswerText = a }).ToList();
        Question question = new Question {
            ImagePath = imageUrl,
            QuestionText = questionText,
            Category = category,
            Answers = answerObjects
        };

        // Mock repositories & their methods
        Mock<IQuestionRepository> questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.Add(It.IsAny<Question>())).Returns(question);
        Mock<IAnswerRepository> answerRepositoryMock = new Mock<IAnswerRepository>();
        answerRepositoryMock.Setup(x => x.Add(It.IsAny<Answer>())).Returns((Answer ans) => ans);

        // Create service
        QuestionService questionService = new QuestionService(questionRepositoryMock.Object, answerRepositoryMock.Object, _mapper);

        // ===== Act =====
        var result = questionService.CreateQuestionWithAnswers(imageUrl, questionText, category, answerObjects);
        
        // ===== Assert =====
        // Expected question DTO
        QuestionDTO questionDto = _mapper.Map<QuestionDTO>(question);
        // Expected response
        result.Should().BeOfType<QuestionDTO>();
        result.Should().BeEquivalentTo(questionDto); // structurally equal (same properties)
        // Verify that the repositories were called
        questionRepositoryMock.Verify(x => x.Add(It.IsAny<Question>()), Times.Once);
        answerRepositoryMock.Verify(x => x.Add(It.IsAny<Answer>()), Times.Exactly(answerObjects.Count));
    }

    [Theory]
    [InlineData("invalid image URL", "What is the capital of France?", "Geography", new[] { "Paris", "London", "Berlin", "Rome" })]
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography", new[] { "Paris", "London", "Berlin"})]
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography", new[] { "Paris", "London", "Berlin", "Rome", "Athens", "New York" })]
    [InlineData("http://www.example.com/image.jpg", "", "Geography", new[] { "Paris", "London", "Berlin", "Rome" })]
    [InlineData("http://www.example.com/image.jpg", null, "Geography", new[] { "Paris", "London", "Berlin", "Rome" })]
    public void CreateQuestionWithAnswers_ShouldThrowException_WhenInvalidDataIsPassed(
        string imageUrl, string questionText, string category,
        string[] answers)
    {
        // ===== Arrange =====
        // Create question with answers
        List<Answer> answerObjects = answers.Select(a => new Answer { AnswerText = a }).ToList();
        Question question = new Question {
            ImagePath = imageUrl,
            QuestionText = questionText,
            Category = category,
            Answers = answerObjects
        };

        // Mock repositories & their methods
        Mock<IQuestionRepository> questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.Add(It.IsAny<Question>())).Returns(question);
        Mock<IAnswerRepository> answerRepositoryMock = new Mock<IAnswerRepository>();
        answerRepositoryMock.Setup(x => x.Add(It.IsAny<Answer>())).Returns((Answer ans) => ans);

        // Create service
        QuestionService questionService = new QuestionService(questionRepositoryMock.Object, answerRepositoryMock.Object, _mapper);

        // ===== Act =====
        Action act = () => questionService.CreateQuestionWithAnswers(imageUrl, questionText, category, answerObjects);
        
        // ===== Assert =====
        act.Should().Throw<Exception>();
    }
}

