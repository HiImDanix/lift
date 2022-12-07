using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.Models;
using GuessingGame.Proxies;

namespace GuessingGame.Repositories;

public class AnswerRepository: IAnswerRepository
{
    private readonly IDbConnection _db;
    private readonly IServiceProvider _provider;
    
    public AnswerRepository(IDbConnection db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }


    public Answer Add(Answer answer)
    {
        try
        {
            var sql = @"INSERT INTO Answers (answer, questionID, isCorrect) VALUES (@AnswerText, @QuestionID, @IsCorrect);SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.QuerySingle<int>(sql, new { AnswerText = answer.AnswerText, QuestionID = answer.Question.Id, IsCorrect = answer.IsCorrect });
            answer.Id = id;
            return ToProxy(answer);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not create answer", e);
        }
    }

    public IList<Answer> GetAnswersForQuestion(int id)
    {
        try
        {
            var sql = @"SELECT id, answer AnswerText, questionID, isCorrect FROM Answers WHERE questionID = @id";
            var answers = _db.Query<Answer>(sql, new { id }).ToList();
            return answers.Select(ToProxy).ToList();
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get answers for question", e);
        }
    }

    public void Update(Answer answer)
    {
        try
        {
            var sql = @"UPDATE Answers SET answer = @AnswerText, questionID = @QuestionID, isCorrect = @IsCorrect WHERE id = @Id";
            _db.Execute(sql, new { AnswerText = answer.AnswerText, QuestionID = answer.Question.Id, IsCorrect = answer.IsCorrect, Id = answer.Id });
        } catch (Exception e)
        {
            throw new DataAccessException("Could not update answer", e);
        }
    }

    // Map answer to proxy
    private Answer ToProxy(Answer answer)
    {
        return new AnswerProxy(_provider.GetRequiredService<IQuestionRepository>())
        {
            Id = answer.Id,
            AnswerText = answer.AnswerText,
            IsCorrect = answer.IsCorrect,
            Question = answer.Question
        };
    }
}