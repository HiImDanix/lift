namespace GuessingGame.Models;

public class QuizGameQuestion
{
    public int Id { get; set; }
    public virtual Question? Question { get; set; }
    public virtual GuessingGameModel? Game { get; set; }
    public virtual List<PlayerAnswer>? Answers { get; set; }
}