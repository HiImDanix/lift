using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;

namespace GuessingGame.Services
{
    public interface IQuestionService
    {
        public QuestionDTO CreateQuestion(string imagePath, string questionText, string category, string answer);
    }
}
