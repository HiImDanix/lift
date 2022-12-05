using desktop_client.ModelLayer;
using desktop_client.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_client.ControlLayer
{
    internal class AnswerController
    {
        public AnswerController()
        {
        }

        private Answer CreateAnswer(string answerText, bool isCorrect)
        {
            Answer newAnswer = new Answer(answerText, isCorrect);
            return newAnswer;
        }

        public List<Answer> CreateAnswerList(List<string> answers)
        {
            List<Answer> answerList = new List<Answer>();
            
            for (int i = 0; i < 4; i++)
            {
                if(i == 0)
                {
                    answerList.Add(CreateAnswer(answers[i], true));
                }
                else
                {
                    answerList.Add(CreateAnswer(answers[i], false));
                }
            }
            return answerList;
        }
    }
}
