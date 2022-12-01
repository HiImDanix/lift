using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_client.ModelLayer
{
    public class Answer
    {
        string AnswerText { get; set; }
        string QuestionId { get; set; }
        bool IsCorrect { get; set; }

        public Answer(string answerText, string questionId, bool isCorrect)
        {
            AnswerText = answerText;
            QuestionId = questionId;
            IsCorrect = isCorrect;
        }
    }
}
