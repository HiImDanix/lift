using AutoMapper;
using Moq;
using FluentAssertions;
using GuessingGame.DTO.responses;
using GuessingGame.Models;
using GuessingGame.Repositories;
using GuessingGame.Services;

namespace tests;

public class QuestionServiceTests
{
    

    [Fact]
    public void CreateQuestionWithAnswers_ShouldAddQuestionAndAnswersToDatabase()
    {
        // Arrange
        var imagePath = "http://www.example.com/image.jpg";
        var questionText = "What is the capital of France?";
        var category = "Geography";
        var answers = new List<Answer> {
            new() { AnswerText = "Paris", IsCorrect = true},
            new() { AnswerText = "London", IsCorrect = false},
            new() { AnswerText = "Berlin", IsCorrect = false},
            new() { AnswerText = "Rome", IsCorrect = false}
        };

        // Question object
        var question = new Question {
            ImagePath = imagePath,
            QuestionText = questionText,
            Category = category,
            Answers = answers
        };
        
        // Answers DTO
        var answersDto = new List<AnswerDTO> {
            new() { AnswerText = "Paris", IsCorrect = true},
            new() { AnswerText = "London", IsCorrect = false},
            new() { AnswerText = "Berlin", IsCorrect = false},
            new() { AnswerText = "Rome", IsCorrect = false}
        };
        // Questions DTO
        var questionDto = new QuestionDTO {
            ImagePath = imagePath,
            QuestionText = questionText,    
            Category = category,
            Answers = answersDto
        };

        // Mock repositories & sub methods
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.Add(question)).Returns(question);
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        answerRepositoryMock.Setup(x => x.Add(It.IsAny<Answer>())).Returns((Answer ans) => ans);
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<QuestionDTO>(question)).Returns(questionDto);
        
        // Create service
        var questionService = new QuestionService(questionRepositoryMock.Object, answerRepositoryMock.Object, mapperMock.Object);
        
        // Mock service method

        // Act
        var result = questionService.CreateQuestionWithAnswers(imagePath, questionText, category, answers);
        
        // Assert
        result.Should().BeOfType<QuestionDTO>();
        result.Should().BeEquivalentTo(questionDto);
        questionRepositoryMock.Verify(x => x.Add(question), Times.Once);
        answerRepositoryMock.Verify(x => x.Add(It.IsAny<Answer>()), Times.Exactly(answers.Count));
        mapperMock.Verify(x => x.Map<QuestionDTO>(question), Times.Once);
    }
}

