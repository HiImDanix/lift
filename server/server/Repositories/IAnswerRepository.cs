using GuessingGame.Models;

namespace GuessingGame.Repositories;

public interface IAnswerRepository
{
    Answer Add(Answer answer);
    IList<Answer> GetAnswersForQuestion(int id);
    void Update(Answer answer);
    Answer GetAnswerByQuizGameAnswerId(int id);
}