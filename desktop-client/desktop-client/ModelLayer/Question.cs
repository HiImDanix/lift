using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_client.ModelLayer
{
    public class Question
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string QuestionText { get; set; }
        public string Category { get; set; }
        public List<Answer> Answers { get; set; }
        public byte[] RowVer { get; set; }
        public Question() { }

        public Question(string imagePath, string question, string category, List<Answer> answers)
        {
            ImagePath = imagePath;
            QuestionText = question;
            Category = category;
            Answers = answers;
        }

        public Question(int id, string imagePath, string questionText, string category, List<Answer> answers, byte[] rowVer)
        {
            Id = id;
            ImagePath = imagePath;
            QuestionText = questionText;
            Category = category;
            Answers = answers;
            RowVer = rowVer;
        }

        public string RowVersion
        {
            get
            {
                if (this.RowVer != null)
                {
                    return Convert.ToBase64String(this.RowVer);
                }
                return string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.RowVer = null;
                }
                else
                {
                    this.RowVer = Convert.FromBase64String(value);
                }
            }
        }

        public override string ToString()
        {
            var correctAnswer = Answers.Find(q => q.IsCorrect == true).AnswerText;
            return QuestionText + " - " + correctAnswer;
        }

    }
}
