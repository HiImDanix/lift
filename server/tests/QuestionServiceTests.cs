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
        string imagePath = "http://www.example.com/image.jpg";
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
            ImagePath = imagePath,
            QuestionText = questionText,
            Category = category,
            Answers = answers
        };

        // Answers DTO
        List<AnswerDTO> answersDto = new List<AnswerDTO> {
            new() { AnswerText = "Paris", IsCorrect = true},
            new() { AnswerText = "London", IsCorrect = false},
            new() { AnswerText = "Berlin", IsCorrect = false},
            new() { AnswerText = "Rome", IsCorrect = false}
        };
        // Questions DTO
        QuestionDTO questionDto = new QuestionDTO {
            ImagePath = imagePath,
            QuestionText = questionText,    
            Category = category,
            Answers = answersDto
        };

        // Mock repositories & sub methods
        Mock<IQuestionRepository> questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.Add(question)).Returns(question);
        Mock<IAnswerRepository> answerRepositoryMock = new Mock<IAnswerRepository>();
        answerRepositoryMock.Setup(x => x.Add(It.IsAny<Answer>())).Returns((Answer ans) => ans);
        Mock<IMapper> mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<QuestionDTO>(question)).Returns(questionDto);

        // Create service
        QuestionService questionService = new QuestionService(questionRepositoryMock.Object, answerRepositoryMock.Object, mapperMock.Object);
        
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

