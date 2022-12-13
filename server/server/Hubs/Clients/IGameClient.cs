using GuessingGame.DTO.responses;

namespace GuessingGame.hubs.Clients;

public interface IGameClient
{
    Task PlayerJoined(PlayerPublicDTO player);
    Task GameStarted(GameDTO game);
    Task RoundStarted(RoundStartDto roundStartDto);
    Task GameFinished();
    Task RoundFinished();
    Task QuestionAnswered(QuestionAnsweredDTO result);
}