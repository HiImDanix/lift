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

        public Answer CreateAnswer(string answerText, string questionId, bool isCorrect)
        {
            Answer newAnswer = new Answer(answerText, questionId, isCorrect);

            return newAnswer;
        }

        public async Task<List<Answer>> groupAnswers(Answer correctAnswer, Answer answer1, Answer answer2, Answer answer3)
        {
            List<Answer> answers = new List<Answer> { 
                correctAnswer,
                answer1, 
                answer2,
                answer3
            };
            return answers;
        }
    }
}
