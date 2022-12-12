using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.Models;
using GuessingGame.Proxies;

namespace GuessingGame.Repositories;

public class GuessingGameRepository: IGuessingGameRepository
{
    
    private readonly IDbConnection _db;
    private readonly IServiceProvider _provider;
    
    public GuessingGameRepository(IDbConnection db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }

    // Note it does not add scoreboard yet
    public GuessingGameModel Add(GuessingGameModel game)
    {
        try
        {
            var sql = @"INSERT INTO QuizGames (roomid, starttime, totalrounds, currentround, currentroundstarttime, status, currentQuizGameQuestionID)
                        VALUES (@RoomId, @StartTime, @TotalRounds, @CurrentRound, @CurrentRoundStartTime, @Status, @CurrentQuizGameQuestionID);
                        ; SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.ExecuteScalar<int>(sql, 
                new { RoomId = game.Room.Id, game.StartTime, game.TotalRounds, game.CurrentRound,
                    game.CurrentRoundStartTime, game.Status, CurrentQuizGameQuestionID = game.CurrentQuizGameQuestion?.Id });
            game.Id = id;
            return ToProxy(game);

        }
        catch (Exception e)
        {
            throw new Exception("Error adding game", e);
        }
    }


    public GuessingGameModel? GetByRoomId(int id)
    {
        // There can be many games for a room, but we only want the current one.
        // So we need to select room first, get 'currentGameId' and then select game by id.
        try
        {
            var sql = @"SELECT * FROM QuizGames WHERE id = (SELECT currentGameID FROM Rooms WHERE id = @id)";
            var game = _db.QuerySingleOrDefault<GuessingGameModel>(sql, new { id });
            return game == null ? null : ToProxy(game);
        } catch (Exception e)
        {
            throw new DataAccessException("Error getting game by room id", e);
        }

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
                    currentQuizGameQuestionID = @CurrentQuizGameQuestionID
                    WHERE id = @Id";
        _db.Execute(sql, model);
    }

    public GuessingGameModel GetGuessingGameByQuizGameQuestionId(int id)
    {
        var sql = @"SELECT * FROM QuizGames WHERE id = (SELECT QuizGameID FROM QuizGameQuestions WHERE id = @id)";
        var game = _db.QueryFirstOrDefault<GuessingGameModel>(sql, new { id });
        return ToProxy(game);
    }

    // Note: it does not add referenced objects
    // Referenced objects must not be null
    public QuizGameQuestion Add(QuizGameQuestion quizGameQuestion)
    {
        var sql = @"INSERT INTO QuizGameQuestions (questionid, quizgameid) VALUES (@QuestionId, @QuizGameId); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = _db.ExecuteScalar<int>(sql, new { @QuestionId = quizGameQuestion.Question.Id, @QuizGameId = quizGameQuestion.Game.Id });
        quizGameQuestion.Id = id;
        return ToProxy(quizGameQuestion);

    }
    
    private QuizGameQuestion ToProxy(QuizGameQuestion question)
    {
        return new QuizGameQuestionproxy(
            _provider.GetRequiredService<IQuestionRepository>(),
            _provider.GetRequiredService<IGuessingGameRepository>())
        {
            Id = question.Id
        };
    }
    
    private GuessingGameModel ToProxy(GuessingGameModel game)
    {
        return new GuessingGameModelProxy(_provider.GetRequiredService<IRoomRepository>())
        {
            Id = game.Id,
            StartTime = game.StartTime,
            TotalRounds = game.TotalRounds,
            CurrentRound = game.CurrentRound,
            CurrentRoundStartTime = game.CurrentRoundStartTime,
            Status = game.Status,
        };

    }
}