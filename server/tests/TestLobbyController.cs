using GuessingGame.Controllers;
using GuessingGame.models;
using Moq;
using FluentAssertions;
using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace tests;

public class TestLobbyController
{

    [Fact]
    public void Post_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var ctrl = new LobbyController(lobbyService: Mock.Of<ILobbyService>());

        // Act
        var result = (OkObjectResult)ctrl.CreateLobby(new CreateRoomRequest("test"));

        // Assert
        result.StatusCode.Should().Be(200);


    }
    
    [Fact]
    public void Post_OnSuccess_CallsCreateRoomAndPlayer()
    {
        // Arrange
        var mockLobbyService = new Mock<ILobbyService>();
        mockLobbyService
            .Setup(x => x.CreateRoomAndPlayer(It.IsAny<string>()))
            .Callback(() => new LobbyCreatedDTO());
        var sut = new LobbyController(mockLobbyService.Object);

        // Act
        var result = (OkObjectResult) sut.CreateLobby(new CreateRoomRequest("test"));

        // Assert
        mockLobbyService.Verify(x => x.CreateRoomAndPlayer(It.IsAny<string>()), Times.Once);
    }
}

