using AutoMapper;
using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.models;
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

        public QuestionDTO CreateQuestionWithAnswers(string imagePath, string questionText,
            string category,
            List<Answer> answers)
        {
            // Map to object
            Question question = new Question()
            {
                ImagePath = imagePath,
                QuestionText = questionText,
                Category = category,
                Answers = answers
            };
            
            // Put question in DB
            question = _questionRepository.Add(question);

            // Add each answer to the database
            // TODO: add questionID to answer. Also, make it so that when you add question, it adds the answers too.
            List<Answer> answersInDb = new List<Answer>();
            foreach (var ans in answers)
            {
                ans.Question = question;
                Answer answer = _answerRepository.Add(ans);
                answersInDb.Add(answer);
            }
            
            // Assign to question as it was retrieved from db before adding answers (to db)
            question.Answers = answersInDb;

            return _mapper.Map<QuestionDTO>(question);
        }

        public IList<QuestionDTO> GetQuestions()
        {
            return _mapper.Map<IList<QuestionDTO>>(_questionRepository.GetAll());
        }
    }
}
