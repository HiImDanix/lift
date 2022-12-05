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
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IAnswerRepository answerRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public Question CreateQuestionWithAnswers(string imagePath, string questionText,
            string category,
            List<Answer> answers)
        {
            
            Question question = new Question(imagePath, questionText, category);
            
            question = _questionRepository.Add(question);

            // TODO: add questionID to answer
            List<Answer> answersInDb = new List<Answer>();
            foreach (var ans in answers)
            {
                ans.Question = question;
                Answer answer = _answerRepository.Add(ans);
                answersInDb.Add(answer);
            }
            
            question.Answers = answersInDb;

            return question;
        }
    }
}
