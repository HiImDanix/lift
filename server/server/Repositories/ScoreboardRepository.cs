using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Proxies;

namespace GuessingGame.Repositories;

public class ScoreboardRepository: IScoreboardRepository
{
    private readonly IDbConnection _db;
    private readonly IServiceProvider _provider;
    
    public ScoreboardRepository(IDbConnection db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }
    
    public List<ScoreboardLine> GetScoreboardByGuessingGameId(int id)
    {
        try
        {
            var sql = @"SELECT * FROM ScoreboardLines WHERE QuizGameID = @id";
            var scoreboardLines = _db.Query<ScoreboardLine>(sql, new {id}).ToList();
            // to proxy for each scoreboardline
            return scoreboardLines.Select(ToProxy).ToList();
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get scoreboard by guessing game id", e);
        }
    }

    public void AddScoreToScoreboard(GuessingGameModel game, Player player, int score)
    {
        try
        {
            var sql = @"INSERT INTO ScoreboardLines (QuizGameID, PlayerID, Score) VALUES (@QuizGameID, @PlayerID, @Score)";
            _db.Execute(sql, new {QuizGameID = game.Id, PlayerID = player.Id, Score = score});
        } catch (Exception e)
        {
            throw new DataAccessException("Could not add score to scoreboard", e);
        }
    }

    public ScoreboardLine? GetScoreForPlayer(GuessingGameModel game, Player player)
    {
        try
        {
            var sql = @"SELECT * FROM ScoreboardLines WHERE QuizGameID = @QuizGameID AND PlayerID = @PlayerID";
            var scoreboardLine = _db.QueryFirstOrDefault<ScoreboardLine>(sql, new {QuizGameID = game.Id, PlayerID = player.Id});
            return scoreboardLine == null ? null : ToProxy(scoreboardLine);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get score for player in game", e);
        }
    }

    public void add(GuessingGameModel game, Player player, int score)
    {
        try
        {
            var sql = @"INSERT INTO ScoreboardLines (QuizGameID, PlayerID, Score) VALUES (@QuizGameID, @PlayerID, @Score)";
            _db.Execute(sql, new {QuizGameID = game.Id, PlayerID = player.Id, Score = score});
        } catch (Exception e)
        {
            throw new DataAccessException("Could not add score to scoreboard", e);
        }
    }

    public void UpdateScoreById(int existingScoreId, int score)
    {
        try
        {
            var sql = @"UPDATE ScoreboardLines SET Score = @Score WHERE Id = @Id";
            _db.Execute(sql, new {Score = score, Id = existingScoreId});
        } catch (Exception e)
        {
            throw new DataAccessException("Could not update score", e);
        }
    }

    private ScoreboardLine ToProxy(ScoreboardLine scoreboard)
    {
        return new ScoreboardLineProxy(
            _provider.GetRequiredService<IPlayerRepository>(),
            _provider.GetRequiredService<IGuessingGameRepository>())
        {
            Id = scoreboard.Id,
            Score = scoreboard.Score
        };
    }
}