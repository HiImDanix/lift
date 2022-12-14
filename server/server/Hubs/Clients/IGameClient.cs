using GuessingGame.DTO.responses;

namespace GuessingGame.hubs.Clients;

public interface IGameClient
{
    Task PlayerJoined(PlayerPublicDTO player);
    Task GameStarted(GameDTO game);
    Task RoundStarted(RoundStartDto roundStartDto);
    Task GameFinished(ScoreboardDTO scoreboard);
    Task RoundFinished(ScoreboardDTO scoreboard);
    Task QuestionAnswered(QuestionAnsweredDTO result);
}