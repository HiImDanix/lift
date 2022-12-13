using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.models;
using GuessingGame.Models;
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
        try
        {
            var sql = @"INSERT INTO Rooms (code) VALUES (@Code);SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.QuerySingle<int>(sql, room);
            room.Id = id;
            return ToProxy(room);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not create room", e);
        }
    }

    public Player AddPlayer(Room room, Player player)
    {
        try
        {
            var sql = @"INSERT INTO Players (session, displayName, roomId) VALUES (@Session, @DisplayName, @RoomId); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.QuerySingle<int>(sql, new { player.Session, player.DisplayName, RoomId = room.Id });
            player.Id = id;
            return ToProxy(player);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not add player to room", e);
        }
    }
    
    // Map Room to RoomProxy
    public RoomProxy ToProxy(Room room)
    {
        // TODO: Use DI to create the proxy?
        return new RoomProxy(_provider.GetRequiredService<IPlayerRepository>(),
            _provider.GetRequiredService<IGuessingGameRepository>())
        {
            Id = room.Id,
            Code = room.Code,
            StartTime = room.StartTime,
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
        try
        {
            var sql = @"SELECT * FROM Rooms WHERE ID = @roomId";
            var room = _db.QuerySingle<Room>(sql, new { roomId });
            return ToProxy(room);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get room by id", e);
        }

        
    }

    public Room? GetByCode(string roomCode)
    {
        try
        {
            var sql = @"SELECT * FROM Rooms WHERE code = @roomCode";
            var room = _db.QuerySingle<Room>(sql, new { roomCode });
            return ToProxy(room);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get room by code", e);
        }
    }

    public Room? Get(int id)
    {
        try
        {
            var sql = @"SELECT * FROM Rooms WHERE id = @id";
            var room = _db.QuerySingle<Room>(sql, new { id });
            return ToProxy(room);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get room by id", e);
        }
    }

    public Room SetHost(Room room, Player player)
    {
        try
        {
            var sql = @"UPDATE Rooms SET hostId = @HostId WHERE id = @roomId";
            _db.Execute(sql, new { @HostId = player.Id, roomId = room.Id });
            room.Host = player;
            return ToProxy(room);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not set host", e);
        }
    }

    public Room UpdateStartTime(Room lobby, long startTime)
    {
        try
        {
            var sql = @"UPDATE Rooms SET startTime = @startTime WHERE id = @roomId";
            _db.Execute(sql, new { startTime, roomId = lobby.Id });
            lobby.StartTime = startTime;
            return ToProxy(lobby);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not update start time", e);
        }
    }

    public void UpdateCurrentGame(Room room, GuessingGameModel model)
    {
        try
        {
            Console.WriteLine("Updating current game");
            var sql = @"UPDATE Rooms SET currentGameID = @currentGameID WHERE id = @roomId";
            _db.Execute(sql, new { currentGameID = model.Id, roomId = room.Id });
        } catch (Exception e)
        {
            throw new DataAccessException("Could not update current game", e);
        }
    }

    public Room? GetRoomByGuessingGameId(int id)
    {
        try
        {
            var sql = @"SELECT * FROM Rooms WHERE id = (SELECT roomId FROM QuizGames WHERE id = @id)";
            var room = _db.QuerySingleOrDefault<Room>(sql, new { id });
            return room == null ? null : ToProxy(room);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get room by guessing game id", e);
        }
    }

    public QuizGameQuestion? GetQuizGameQuestionByGuessingGameId(int id)
    {
        try
        {
            var sql = @"SELECT * from QuizGameQuestions where id = (SELECT currentQuizGameQuestionID FROM QuizGames WHERE id = @id)";
            var question = _db.QuerySingleOrDefault<QuizGameQuestion>(sql, new { id });
            return question == null ? null : ToProxy(question);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get quiz game question by guessing game id", e);
        }
    }
    
    private QuizGameQuestion ToProxy(QuizGameQuestion question)
    {
        return new QuizGameQuestionproxy(
            _provider.GetRequiredService<IQuestionRepository>(),
            _provider.GetRequiredService<IGuessingGameRepository>(),
        _provider.GetRequiredService<IPlayerAnswersRepository>())
        {
            Id = question.Id
        };
    }

    public List<QuizGameQuestion> GetQuizGameQuestionsByGuessingGameId(int id)
    {
        try
        {
            var sql = @"SELECT * FROM QuizGameQuestions WHERE QuizGameID = @id";
            var questions = _db.Query<QuizGameQuestion>(sql, new { id }).ToList();
            return questions;
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get quiz game questions by guessing game id", e);
        }
    }
}