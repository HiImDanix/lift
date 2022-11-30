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

        public QuestionController()
        {
            _qService = new QuestionService();
        }
        public async Task<int> SaveQuestion(string imagePath, string question, string category, string answer)
        {
            int insertId = -1;
            Question newQuestion = new Question(imagePath, question, category, answer);

            insertId = await _qService.SaveQuestion(newQuestion);
            return insertId;
        }
    }
}
