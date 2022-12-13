using GuessingGame.models;

namespace GuessingGame.Models;

public class PlayerAnswer
{
    public int Id { get; set; }
    public virtual Player? Player { get; set; }
    public virtual Answer? Answer { get; set; }
    public virtual QuizGameQuestion? QuizGameQuestion { get; set; }
    public long AnsweredTime { get; set; }
    
}