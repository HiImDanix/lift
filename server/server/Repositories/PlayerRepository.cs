using System.Data;
using Dapper;
using GuessingGame.models;
using GuessingGame.Proxies;
using Microsoft.Data.SqlClient;

namespace GuessingGame.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly IDbConnection _db;
    // inject provider
    private readonly IServiceProvider _provider;
    
    public PlayerRepository(IDbConnection db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }

    public Player Create(Player player)
    {
        var sql = "INSERT INTO Player (session, displayName) VALUES (@DisplayName, @Session); SELECT SCOPE_IDENTITY()";
        
        IDbCommand cmd = _db.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.Add(new SqlParameter("@DisplayName", player.DisplayName));
        cmd.Parameters.Add(new SqlParameter("@Session", player.Session));

        // execute and get the auto-generated ID / PK
        player.PlayerId = (int)(decimal)cmd.ExecuteScalar();
        return player;
    }

    public Player? Get(int id)
    {
        var sql = "SELECT * FROM Players WHERE id = @id";
        
        // get DataReader
        var reader = _db.ExecuteReader(sql, new { id });
        return null;
    }

    public IList<Player> GetPlayersByRoomId(int id)
    {
        var sql = "SELECT * FROM Player WHERE roomID = @id";
        
        var players = _db.Query<Player>(sql, new { id }).ToList();
        // map each player to player proxy using ToProxy(player) method
        return players.Select(ToProxy).ToList();
    }
    
    // Map Player to PlayerProxy
    private Player ToProxy(Player player)
    {
        return new PlayerProxy(_provider.GetRequiredService<IRoomRepository>())
        {
            PlayerId = player.PlayerId,
            Session = player.Session,
            DisplayName = player.DisplayName,
        };
    }
}