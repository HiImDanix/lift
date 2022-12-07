using GuessingGame.models;
using GuessingGame.Models;

namespace GuessingGame.Repositories
{
    public interface IQuestionRepository
    {
        Question Add(Question question);
        Question GetQuestionByAnswerId(int id);
        IList<Question> GetAll();
        Question Get(int id);
        Question Update(Question question);
        void RemoveAnswers(Question question);
    }
}
