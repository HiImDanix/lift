using AutoMapper;
using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public QuestionDTO CreateQuestion(string imagePath, string questionText, string category, string answer)
        {
            Question question = new Question(imagePath, questionText, category, answer);
            return _mapper.Map<QuestionDTO>(question);
        }
    }
}
