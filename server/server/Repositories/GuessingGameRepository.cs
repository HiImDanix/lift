using System.Data;
using Dapper;
using GuessingGame.Models;

namespace GuessingGame.Repositories;

public class GuessingGameRepository: IGuessingGameRepository
{
    
    private readonly IDbConnection _db;
    
    public GuessingGameRepository(IDbConnection db)
    {
        _db = db;
    }
    
    // Note it does not add scoreboard yet
    // TODO: Use proxy
    public GuessingGameModel Add(GuessingGameModel game)
    {
        try
        {
            var sql = @"INSERT INTO QuizGames (roomid, starttime, totalrounds, currentround, currentroundstarttime, status, currentquestionid)
                        VALUES (@RoomId, @StartTime, @TotalRounds, @CurrentRound, @CurrentRoundStartTime, @Status, @CurrentQuestionId);
                        ; SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.ExecuteScalar<int>(sql, 
                new { RoomId = game.Room.Id, game.StartTime, game.TotalRounds, game.CurrentRound,
                    game.CurrentRoundStartTime, game.Status, CurrentQuestionId = game.CurrentQuestion?.Id });
            game.Id = id;
            return game;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // TODO: Use proxy
    public GuessingGameModel GetByRoomId(int id)
    {
        // There can be many games for a room, but we only want the current one.
        // So we need to select room first, get 'currentGameId' and then select game by id.
        var sql = @"SELECT * FROM QuizGames WHERE id = (SELECT currentgameid FROM Rooms WHERE id = @id)";
        return _db.QueryFirstOrDefault<GuessingGameModel>(sql, new { id });
    }

    /**
     * Updates game model along with current question.
     * it does not update current room.
     */
    public void Update(GuessingGameModel model)
    {
        var sql = @"UPDATE QuizGames SET 
                    starttime = @StartTime, 
                    totalrounds = @TotalRounds, 
                    currentround = @CurrentRound, 
                    currentroundstarttime = @CurrentRoundStartTime, 
                    status = @Status, 
                    currentquestionid = @CurrentQuestionId
                    WHERE id = @Id";
        _db.Execute(sql, model);
    }
}