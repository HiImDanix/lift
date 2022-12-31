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
    // Normal case
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography",
        new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 0, 0, 0 })]
    // No image case
    [InlineData("", "What is the capital of France?", "Geography",
        new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 0, 0, 0, 1 })]
    // Multiple correct answers case
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography",
        new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 1, 0, 0 })]
    // All correct answers case
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography",
        new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 1, 1, 1 })]
    // No category case
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "",
        new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 0, 0, 0 })]
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", null,
        new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 0, 0, 0 })]
    public void CreateQuestionWithAnswers_ShouldAddQuestionAndAnswersToDatabase(
        string imageUrl, string questionText, string category,
        string[] answers, int[] correctAnswers)
    {
        // ===== Arrange =====
        // Create question with answers
        List<Answer> answerObjects = answers.Select(a => new Answer { AnswerText = a }).ToList();
        for (int i = 0; i < correctAnswers.Length; i++)
        {
            if (correctAnswers[i] == 1)
            {
                answerObjects[i].IsCorrect = true;
            }
        }
        Question question = new Question {
            ImagePath = imageUrl,
            QuestionText = questionText,
            Category = category,
            Answers = answerObjects
        };
        
        // Expected question DTO
        QuestionDTO questionDto = _mapper.Map<QuestionDTO>(question);

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
        result.Should().BeOfType<QuestionDTO>();
        result.Should().BeEquivalentTo(questionDto); // structurally equal (same properties)
        // Expected answer DTOs
        result.Answers.Should().BeOfType<List<AnswerDTO>>();
        result.Answers.Should().BeEquivalentTo(questionDto.Answers); // structurally equal (same properties)
        // Verify that the repositories were called
        questionRepositoryMock.Verify(x => x.Add(It.IsAny<Question>()), Times.Once);
        answerRepositoryMock.Verify(x => x.Add(It.IsAny<Answer>()), Times.Exactly(answerObjects.Count));
    }

    [Theory]
    // Invalid image URL
    [InlineData("invalid image URL", "What is the capital of France?", "Geography", new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 0, 0, 0 })]
    // 3 answers (should be 4)
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography", new[] { "Paris", "London", "Berlin"}, new[] { 1, 0, 0 })]
    // 5 answers (should be 4)
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography", new[] { "Paris", "London", "Berlin", "Rome", "Athens", "New York" }, new[] { 1, 0, 0, 0, 0, 0 })]
    // Invalid question text
    [InlineData("http://www.example.com/image.jpg", "", "Geography", new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 0, 0, 0 })]
    [InlineData("http://www.example.com/image.jpg", null, "Geography", new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 1, 0, 0, 0 })]
    // No correct answers (should be at least 1)
    [InlineData("http://www.example.com/image.jpg", "What is the capital of France?", "Geography", new[] { "Paris", "London", "Berlin", "Rome" }, new[] { 0, 0, 0, 0 })]
    public void CreateQuestionWithAnswers_ShouldThrowException_WhenInvalidDataIsPassed(
        string imageUrl, string questionText, string category,
        string[] answers, int[] correctAnswers)
    {
        // ===== Arrange =====
        // Create question with answers
        List<Answer> answerObjects = answers.Select(a => new Answer { AnswerText = a }).ToList();
        for (int i = 0; i < correctAnswers.Length; i++)
        {
            if (correctAnswers[i] == 1)
            {
                answerObjects[i].IsCorrect = true;
            }
        }
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

