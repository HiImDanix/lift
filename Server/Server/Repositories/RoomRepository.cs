using System.Data;
using Dapper;
using GuessingGame.models;
using GuessingGame.Proxies;
using Microsoft.Data.SqlClient;

namespace GuessingGame.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly IDbConnection _db;
    private readonly IPlayerRepository _playerRepository;

    public RoomRepository(IDbConnection db,IPlayerRepository playerRepository)
    {
        _db = db;
        _playerRepository = playerRepository;
    }

    public Room Create(Room room)
    {
        var sql = @"INSERT INTO Room (code) VALUES (@Code);SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.QuerySingle<int>(sql, room);
        room.Id = id;
        return ToProxy(room);
    }

    public Room AddPlayer(Room room, Player player)
    {
        var sql = @"INSERT INTO Player (session, displayName, roomId) VALUES (@Session, @DisplayName, @RoomId); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.QuerySingle<int>(sql, new { player.Session, player.DisplayName, RoomId = room.Id });
        player.PlayerId = id;
        player = ToProxy(player);
        room.Players = room.Players.Append(player);
        return ToProxy(room);
    }
    
    // Map Room to RoomProxy
    public RoomProxy ToProxy(Room room)
    {
        return new RoomProxy(_playerRepository)
        {
            Id = room.Id,
            Code = room.Code,
        };
    }
    
    // Map Player to PlayerProxy
    private Player ToProxy(Player player)
    {
        return new PlayerProxy(this)
        {
            PlayerId = player.PlayerId,
            Session = player.Session,
            DisplayName = player.DisplayName,
        };
    }

    public Room GetRoomById(object roomId)
    {
        var sql = @"SELECT * FROM Room WHERE roomID = @roomId";
        var room = _db.QuerySingle<Room>(sql, new { roomId });
        return ToProxy(room);
    }
    
}