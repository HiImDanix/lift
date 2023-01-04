using GuessingGame.models;
using GuessingGame.Models;

namespace GuessingGame.Repositories;

public interface IScoreboardRepository
{
    List<ScoreboardLine> GetScoreboardByGuessingGameId(int id);
    void AddScoreToScoreboard(GuessingGameModel game, Player player, int score);
    ScoreboardLine? GetScoreForPlayer(GuessingGameModel game, Player player);
    void add(GuessingGameModel game, Player player, int score);
    void UpdateScoreById(int existingScoreId, int score);
}