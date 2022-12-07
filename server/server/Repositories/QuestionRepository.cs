using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Proxies;

namespace GuessingGame.Repositories;

public class QuestionRepository : IQuestionRepository
{

    private readonly IDbConnection _db;
    private readonly IServiceProvider _provider;
    
    public QuestionRepository(IDbConnection db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }
    
    public Question Add(Question question)
    {
        try
        {
            var sql = @"INSERT INTO Questions (imgPath, question, category) VALUES (@ImagePath, @QuestionText, @Category);SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.QuerySingle<int>(sql, question);
            question.Id = id;
            return ToProxy(question);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not create question", e);
        }
    }

    public Question GetQuestionByAnswerId(int id)
    {
        try
        {
            var sql = @"SELECT * FROM Questions WHERE id = (SELECT questionId FROM Answers WHERE id = @id)";
            var question = _db.QuerySingle<Question>(sql, new { id });
            return ToProxy(question);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get question by answer id", e);
        }
    }

    public IList<Question> GetAll()
    {
        try
        {
            //sql with aliases e.g. question should Become QuestionText, imgPath should become ImagePath
            var sql = @"SELECT id, imgPath ImagePath, question QuestionText, category Category FROM Questions";
            var questions = _db.Query<Question>(sql).ToList();
            return questions.Select(ToProxy).ToList();
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get all questions", e);
        }
    }

    public Question Get(int id)
    {
        try
        {
            var sql = @"SELECT id, imgPath ImagePath, question QuestionText, category Category, RowVer FROM Questions WHERE id = @id";
            var question = _db.QuerySingle<Question>(sql, new { id });
            return ToProxy(question);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not get question by id", e);
        }
    }

    public Question Update(Question question)
    {
        try
        {
            var sql = @"UPDATE Questions SET imgPath = @ImagePath, question = @QuestionText, category = @Category WHERE id = @Id";
            _db.Execute(sql, question);
            return ToProxy(question);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not update question", e);
        }
    }

    public void RemoveAnswers(Question question)
    {
        try
        {
            var sql = @"DELETE FROM Answers WHERE questionId = @Id";
            _db.Execute(sql, question);
        } catch (Exception e)
        {
            throw new DataAccessException("Could not remove answers", e);
        }
    }

    private Question ToProxy(Question question)
    {
        return new QuestionProxy(_provider.GetRequiredService<IAnswerRepository>())
        {
            Id = question.Id,
            ImagePath = question.ImagePath,
            QuestionText = question.QuestionText,
            Category = question.Category,
        };
    }
}