using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Proxies;

namespace GuessingGame.Repositories;

public interface IPlayerAnswersRepository
{
    bool IsAnswered(Player player, QuizGameQuestion question);
    void Add(PlayerAnswer playerAnswer);
    List<PlayerAnswer> GetPlayerAnswersByQuizGameQuestionId(int id);
}

public class PlayerAnswersRepository: IPlayerAnswersRepository
{
    private readonly IDbConnection _connection;
    // provider
    private readonly IServiceProvider _provider;
    
    public PlayerAnswersRepository(IDbConnection connection, IServiceProvider provider)
    {
        _connection = connection;
        _provider = provider;
    }

    public bool IsAnswered(Player player, QuizGameQuestion question)
    {
        // Check if Table QuizGameAnswers  has a record with playerId and quizGameQuestionId
        var sql = "SELECT COUNT(*) FROM QuizGameAnswers WHERE PlayerId = @playerId AND quizGameQuestionID = @quizGameQuestionId";
        var count = _connection.ExecuteScalar<int>(sql, new {playerId = player.Id, quizGameQuestionId = question.Id});
        return count > 0;
        
    }

    public void Add(PlayerAnswer playerAnswer)
    {
        // Insert a new record into QuizGameAnswers
        try
        {
            var sql =
                "INSERT INTO QuizGameAnswers (playerID, quizGameQuestionID, answerID, answeredTime) VALUES (@PlayerId, @QuizGameQuestionId, @AnswerId, @AnsweredTime)";
            _connection.Execute(sql, new
            {
                PlayerId = playerAnswer.Player.Id,
                QuizGameQuestionId = playerAnswer.QuizGameQuestion.Id,
                AnswerId = playerAnswer.Answer.Id,
                playerAnswer.AnsweredTime
            });
        }
        catch (Exception e)
        {
            throw new DataAccessException("Error while inserting a a player's answer", e);
        }

    }

    public List<PlayerAnswer> GetPlayerAnswersByQuizGameQuestionId(int id)
    {
        // Get all the answers for a specific question
        var sql = "SELECT * FROM QuizGameAnswers WHERE quizGameQuestionID = @id";
        var playerAnswers = _connection.Query<PlayerAnswer>(sql, new {id}).ToList();
        return playerAnswers.Select(ToProxy).ToList();
    }
    
    private PlayerAnswer ToProxy(PlayerAnswer answer)
    {
        return new PlayerAnswerProxy(_provider.GetRequiredService<IQuestionRepository>(),
            _provider.GetRequiredService<IPlayerRepository>(),
            _provider.GetRequiredService<IAnswerRepository>())
        {
            Id = answer.Id,
            AnsweredTime = answer.AnsweredTime
        };
    }
}

