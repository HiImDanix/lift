using GuessingGame.Models;

namespace GuessingGame.Repositories;

public interface IGuessingGameRepository
{
    public GuessingGameModel Add(GuessingGameModel game);
    GuessingGameModel? GetByRoomId(int id);
    void Update(GuessingGameModel model);
    GuessingGameModel GetGuessingGameByQuizGameQuestionId(int id);
    QuizGameQuestion Add(QuizGameQuestion quizGameQuestion);
}