using System.Data;
using Dapper;
using GuessingGame.models;
using GuessingGame.Proxies;
using Microsoft.Data.SqlClient;

namespace GuessingGame.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly IDbConnection _db;
    private readonly IServiceProvider _provider;
    
    public PlayerRepository(IDbConnection db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }

    public Player? Get(int id)
    {
        var sql = "SELECT * FROM Players WHERE ID = @id";
        var player = _db.QueryFirstOrDefault<Player>(sql, new {id});
        return player == null ? null : ToProxy(player);
    }

    public IList<Player> GetPlayersByRoomId(int id)
    {
        var sql = "SELECT * FROM Players WHERE roomID = @id";
        var players = _db.Query<Player>(sql, new { id }).ToList();
        // map each player to player proxy using ToProxy(player) method
        return players.Select(ToProxy).ToList();
    }

    public Player GetHostByRoomId(int id)
    {
        //select hostID from Rooms by roomID, then retrieve player by hostID
        var sql = "SELECT * FROM Players WHERE ID = (SELECT hostID FROM Rooms WHERE ID = @id)";
        var host = _db.QueryFirstOrDefault<Player>(sql, new { id });
        return ToProxy(host);
    }

    public Player GetPlayerByQuizGameAnswerId(int id)
    {
        //select playerID from QuizGameAnswers by answerID, then retrieve player by playerID
        var sql = "SELECT * FROM Players WHERE ID = (SELECT playerID FROM QuizGameAnswers WHERE ID = @id)";
        var player = _db.QueryFirstOrDefault<Player>(sql, new { id });
        return ToProxy(player);
    }

    public Player GetPlayerByScoreboardLineId(int id)
    {
        //select playerID from ScoreboardLines by lineID, then retrieve player by playerID
        var sql = "SELECT * FROM Players WHERE ID = (SELECT playerID FROM ScoreboardLines WHERE ID = @id)";
        var player = _db.QueryFirstOrDefault<Player>(sql, new { id });
        return ToProxy(player);
    }

    // Map Player to PlayerProxy
    private Player ToProxy(Player player)
    {
        return new PlayerProxy(_provider.GetRequiredService<IRoomRepository>())
        {
            Id = player.Id,
            Session = player.Session,
            DisplayName = player.DisplayName,
        };
    }
}