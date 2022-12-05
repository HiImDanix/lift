using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.Models;

namespace GuessingGame.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {

        private readonly IDbConnection _db;
        
        public QuestionRepository(IDbConnection db)
        {
            _db = db;
        }
        
        public Question Add(Question question)
        {
            try
            {
                var sql = @"INSERT INTO Questions (imgPath, question, category) VALUES (@ImagePath, @QuestionText, @Category);SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = _db.QuerySingle<int>(sql, question);
                question.Id = id;
                return question;
                // TODO: Implement proxy
                // return ToProxy(room);
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
                return _db.QuerySingle<Question>(sql, new { id });
            } catch (Exception e)
            {
                throw new DataAccessException("Could not get question by answer id", e);
            }
        }
    }
}
