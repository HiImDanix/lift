using System.Data;
using Dapper;
using GuessingGame.models;
using GuessingGame.Proxies;
using Microsoft.Data.SqlClient;

namespace GuessingGame.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly IDbConnection _db;
    private readonly IServiceProvider _provider;

    public RoomRepository(IDbConnection db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }

    public Room Create(Room room)
    {
        var sql = @"INSERT INTO Rooms (code) VALUES (@Code);SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.QuerySingle<int>(sql, room);
        room.Id = id;
        return ToProxy(room);
    }

    public Room AddPlayer(Room room, Player player)
    {
        var sql = @"INSERT INTO Players (session, displayName, roomId) VALUES (@Session, @DisplayName, @RoomId); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.QuerySingle<int>(sql, new { player.Session, player.DisplayName, RoomId = room.Id });
        player.Id = id;
        player = ToProxy(player);
        room.Players = room.Players.Append(player);
        return ToProxy(room);
    }
    
    // Map Room to RoomProxy
    public RoomProxy ToProxy(Room room)
    {
        // TODO: Use DI to create the proxy?
        return new RoomProxy(_provider.GetRequiredService<IPlayerRepository>())
        {
            Id = room.Id,
            Code = room.Code,
        };
    }
    
    // Map Player to PlayerProxy
    private Player ToProxy(Player player)
    {
        // TODO: Use DI to create the proxy?
        return new PlayerProxy(_provider.GetRequiredService<IRoomRepository>())
        {
            Id = player.Id,
            Session = player.Session,
            DisplayName = player.DisplayName,
        };
    }

    public Room GetRoomById(object roomId)
    {
        var sql = @"SELECT * FROM Rooms WHERE ID = @roomId";
        var room = _db.QuerySingle<Room>(sql, new { roomId });
        return ToProxy(room);
    }

}