using AutoMapper;
using GuessingGame.DTO.requests;
using GuessingGame.DTO.responses;
using GuessingGame.Exceptions;
using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;
using InvalidDataException = GuessingGame.Exceptions.InvalidDataException;

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

        /// <summary>
        /// This method creates a question with the given image path, question text, and category,
        /// along with the answers to the question, and adds it to the database.
        /// </summary>
        /// <param name="imagePath">The URL to the image associated with the question</param>
        /// <param name="questionText">The text of the question.</param>
        /// <param name="category">The category of the question.</param>
        /// <param name="answers">The answers to the question.</param>
        /// <returns>A DTO of the created question with the answers added to the database.</returns>
        public QuestionDTO CreateQuestionWithAnswers(string imagePath, string questionText,
            string category,
            List<Answer> answers)
        {
            
            // Validation - imagePath must be a valid URL
            if (!Uri.IsWellFormedUriString(imagePath, UriKind.Absolute))
            {
                throw new InvalidDataException("The image path must be a valid URL.");
            }
            
            // Validation - questionText must not be empty
            if (string.IsNullOrWhiteSpace(questionText))
            {
                throw new InvalidDataException("The question text must not be empty.");
            }
            
            // Validation - Must have exactly 4 answers
            if (answers.Count != 4)
            {
                throw new InvalidDataException("There must be exactly 4 answers.");
            }


            // Add the question to the database
            var question = AddQuestionToDatabase(imagePath, questionText, category);
            
            // Add the answers to the database & associate them with the question
            AddAnswersToDatabase(question, answers);

            return MapToDto(question);
        }

        private Question AddQuestionToDatabase(string imagePath, string questionText, string category)
        {
            Question question = new Question()
            {
                ImagePath = imagePath,
                QuestionText = questionText,
                Category = category
            };
            
            // Put question in DB
            return _questionRepository.Add(question);
        }
        
        private void AddAnswersToDatabase(Question question, List<Answer> answers)
        {
            // Add each answer to the database
            List<Answer> answersInDb = new List<Answer>();
            foreach (var ans in answers)
            {
                ans.Question = question;
                Answer answer = _answerRepository.Add(ans);
                answersInDb.Add(answer);
            }

            // Assign db answers to question as well (the other way around)
            question.Answers = answersInDb;
        }

        public IList<QuestionDTO> GetQuestions()
        {
            return _mapper.Map<IList<QuestionDTO>>(_questionRepository.GetAll());
        }

        public QuestionDTO UpdateQuestionWithAnswers(int id, string requestImagePath, string requestQuestionText,
            string requestCategory, byte[] requestRowVer, List<Answer> answersList)
        {
            // Get question from the database
            Question question = _questionRepository.Get(id);
            
            // Throw exception if question is null
            if (question == null)
            {
                throw new Exception("Question not found");
            }
            
            // Update question
            UpdateQuestion(question, requestImagePath, requestQuestionText, requestCategory, requestRowVer);
            
            // Update answers
            UpdateAnswers(question, answersList);
            
            // Map to DTO
            return MapToDto(question);
        }
        
        private void UpdateQuestion(Question question, string requestImagePath, string requestQuestionText,
            string requestCategory, byte[] requestRowVer)
        {
            question.ImagePath = requestImagePath;
            question.QuestionText = requestQuestionText;
            question.Category = requestCategory;
            question.RowVer = requestRowVer;
            
            // Update question in database
            _questionRepository.Update(question);
        }
        
        private void UpdateAnswers(Question question, List<Answer> answersList)
        {
            // Clear answers
            question.Answers.Clear();
            _questionRepository.RemoveAnswers(question);
            
            // Add each answer to the database
            foreach (var ans in answersList)
            {
                ans.Question = question;
                Answer answer = _answerRepository.Add(ans);
                question.Answers.Add(answer);
            }
        }
        
        private QuestionDTO MapToDto(Question question)
        {
            return _mapper.Map<QuestionDTO>(question);
        }
    }
}
