using System.Data;
using Dapper;
using GuessingGame.models;
using Microsoft.Data.SqlClient;

namespace GuessingGame.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly string _connString;
    
    public RoomRepository(string connString)
    {
        _connString = connString;
    }


    public Room Create(Room room)
    {
        using var conn = new SqlConnection(_connString);
        var sql = @"INSERT INTO Room (code) VALUES (@Code);SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = conn.QuerySingle<int>(sql, room);
        room.Id = id;
        return room;
    }

    public Player AddPlayer(Room room, Player player)
    {
        using var conn = new SqlConnection(_connString);
        var sql = @"INSERT INTO Player (session, displayName, roomId) VALUES (@Session, @DisplayName, @RoomId); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = conn.QuerySingle<int>(sql, player);
        player.Id = id;
        // add playerID to room.Players
        room.Players = room.Players.Append(player.RoomId);
        return player;
    }
}