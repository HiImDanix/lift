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

        public Task<List<Question>> GetQuestions()
        {
            Task<List<Question>> questions =  _qService.GetQuestions();
            return questions;
        } 

        public async Task<int> EditQuestion(int id, string imagePath, string question, string category, List<string> answerStrings, byte[] rowVer)
        {
            List<Answer> answers = _answerController.CreateAnswerList(answerStrings);
            Question newQuestion = new Question(id, imagePath, question, category, answers, rowVer);

            int responseCode = await _qService.EditQuestion(newQuestion);

            return responseCode;
        }
    }
}
