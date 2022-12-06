using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_client.ModelLayer
{
    public class Answer
    {
        //TODO change answer field name
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public Answer(string answerText, bool isCorrect)
        {
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }
    }
}
