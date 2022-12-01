using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.Models;

namespace GuessingGame.Repositories;

public class AnswerRepository: IAnswerRepository
{
    private readonly IDbConnection _db;
    
    public AnswerRepository(IDbConnection db)
    {
        _db = db;
    }


    public Answer Add(Answer answer)
    {
        try
        {
            var sql = @"INSERT INTO Answers (answer, questionID, isCorrect) VALUES (@AnswerText, @QuestionID, @IsCorrect);SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.QuerySingle<int>(sql, new { AnswerText = answer.AnswerText, QuestionID = answer.Question.Id, IsCorrect = answer.IsCorrect });
            answer.Id = id;
            return answer;
            // TODO: Implement proxy
            // return ToProxy(room);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not create answer", e);
        }
    }
}