using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.Models;

namespace GuessingGame.Services
{
    public interface IQuestionService
    {
        public Question CreateQuestionWithAnswers(string imagePath,
            string questionText,
            string category,
            List<Answer> answers);
    }
}
