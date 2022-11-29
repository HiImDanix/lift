namespace Server.Models
{
    public class Question
    {
            public string ImagePath { get; set; }
            public string QuestionText { get; set; }
            public string Category { get; set; }
            public string Answer { get; set; }
            public Question() { }

            public Question(string imagePath, string question, string category, string answer)
            {
                ImagePath = imagePath;
                QuestionText = question;
                Category = category;
                Answer = answer;
            }



        }
    }
