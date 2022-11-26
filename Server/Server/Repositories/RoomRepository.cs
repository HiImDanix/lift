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
        // insert db. if fails, throw exception
        var id = conn.QuerySingle<int>(sql, room);
        // print id
        System.Console.WriteLine("id: " + id);
        
        room.Id = id;
        return room;
    }

    public void AddPlayer(Room room, Player player)
    {
        throw new NotImplementedException();
    }
}