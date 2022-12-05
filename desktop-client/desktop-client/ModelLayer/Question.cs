using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_client.ModelLayer
{
    public class Question
    {
        public string ImagePath { get; set; }
        public string QuestionText { get; set; }
        public string Category { get; set; }
        public List<Answer> AnswerList { get; set; }
        public Question() { }

        public Question(string imagePath, string question, string category, List<Answer> answerList)
        {
            ImagePath = imagePath;
            QuestionText = question;
            Category = category;
            AnswerList = answerList;
        }


       
    }
}
