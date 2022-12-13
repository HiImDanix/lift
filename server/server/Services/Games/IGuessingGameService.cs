using GuessingGame.DTO.responses;
using GuessingGame.Models;

namespace GuessingGame.Services;

public interface IGuessingGameService
{
    public GuessingGameModel StartGame(GuessingGameModel model);
    public QuestionAnsweredDTO AnswerQuestion(int playerId, int quizGameQuestionID, string answer);
}