using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desktop_client.ModelLayer;

namespace desktop_client.ControlLayer
{
    internal class QuestionController
    {
        public async Task<int> SaveQuestion(string imagePath, string question, string category, string answer)
        {
            int insertId = -1;
            Question newQuestion = new Question(imagePath, question, category, answer);
            return insertId;

            throw new NotImplementedException();
        }
    }
}
