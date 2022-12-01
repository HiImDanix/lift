namespace GuessingGame.Models
{
    public class Question
    {
        public Question(string imagePath, string questionText, string category)
        {
            ImagePath = imagePath;
            QuestionText = questionText;
            Category = category;
        }

        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string QuestionText { get; set; }
        public string Category { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}
