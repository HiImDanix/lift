namespace GuessingGame.Models
{
    public class Question
    {

        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string QuestionText { get; set; }
        public string Category { get; set; }
        public virtual IList<Answer>? Answers { get; set; }
        public byte[] RowVer { get; set; }
    }
}
