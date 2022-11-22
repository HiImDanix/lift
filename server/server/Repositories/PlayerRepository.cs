using System.Data;
using Dapper;
using GuessingGame.models;
using Microsoft.Data.SqlClient;

namespace GuessingGame.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly string _connString;
    
    public PlayerRepository(string connString)
    {
        _connString = connString;
    }


    public Player Create(Player player)
    {
        var sql = "INSERT INTO Players (session, displayName) VALUES (@DisplayName, @Session); SELECT SCOPE_IDENTITY()";

        using (var db = new SqlConnection(_connString))
        {
            db.Open();
            IDbCommand cmd = db.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@DisplayName", player.DisplayName));
            cmd.Parameters.Add(new SqlParameter("@Session", player.Session));

            // execute and get the auto-generated ID / PK
            player.Id = (int)(decimal)cmd.ExecuteScalar();
        }
        return player;
    }

    public Player? Get(int id)
    {
        var sql = "SELECT * FROM Players WHERE id = @id";
        using (var db = new SqlConnection(_connString))
        {
            db.Open();
            // get DataReader
            var reader = db.ExecuteReader(sql, new { id });
            // map to object
            while (reader.Read())
            {
                return new Player(
                    id: reader.GetInt32(reader.GetOrdinal("ID")),
                    displayName: reader.GetString(reader.GetOrdinal("displayName")),
                    session: reader.GetString(reader.GetOrdinal("session"))
                );
            }
        }
        return null;
    }
}