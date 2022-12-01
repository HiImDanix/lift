namespace GuessingGame.Models;

public class Answer
{
    public int Id { get; set; }
    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public virtual Question? Question { get; set; }
}