using GuessingGame.models;

namespace GuessingGame.Models;

public class PlayerAnswer
{
    public Player Player { get; set; }
    public Answer Answer { get; set; }
    public QuizGameQuestion QuizGameQuestion { get; set; }
    public long AnsweredTime { get; set; }
    
}