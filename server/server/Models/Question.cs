namespace GuessingGame.Models
{
    public class Question
    {
        public Question(string imagePath, string questionText, string category, string answer)
        {
            ImagePath = imagePath;
            QuestionText = questionText;
            Category = category;
            Answer = answer;
        }

        public string ImagePath { get; set; }
        public string QuestionText { get; set; }
        public string Category { get; set; }
        public string Answer { get; set; }
    }
}
