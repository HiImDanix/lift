using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.Models;

namespace GuessingGame.Services
{
    public interface IQuestionService
    {
        public QuestionDTO CreateQuestionWithAnswers(string imagePath,
            string questionText,
            string category,
            List<Answer> answers);

        IList<QuestionDTO> GetQuestions();
    }
}
