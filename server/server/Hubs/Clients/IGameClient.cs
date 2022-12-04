using GuessingGame.DTO.responses;

namespace GuessingGame.hubs.Clients;

public interface IGameClient
{
    Task PlayerJoined(PlayerPublicDTO player);
    Task GameStarted();
}