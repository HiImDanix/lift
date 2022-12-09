using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desktop_client.ModelLayer;

using desktop_client.ServiceLayer;


namespace desktop_client.ControlLayer
{
    internal class QuestionController
    {

        QuestionService _qService;
        AnswerController _answerController;

        public QuestionController()
        {
            _qService = new QuestionService();
            _answerController = new AnswerController();
        }

        public async Task<int> SaveQuestion(string imagePath, string question, string category, List<string> answerStrings)
        {
            int insertId = -1;
            List<Answer> answers = _answerController.CreateAnswerList(answerStrings);
            Question newQuestion = new Question(imagePath, question, category, answers);

            insertId = await _qService.SaveQuestion(newQuestion);

            return insertId;
        }

        public async Task<List<Question>> GetQuestions()
        {
            List<Question> questions = await _qService.GetQuestions();
            return questions;
        } 
    }
}
