using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_client.ControlLayer
{
    internal class QuestionController
    {
        public async Task<int> SaveQuestion()
        {
            int insertId = -1;
            return insertId;
        }

        internal Task<int> SaveQuestion(string imagePath, string question, string category, string answer)
        {
            throw new NotImplementedException();
        }
    }
}
