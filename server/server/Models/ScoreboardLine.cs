using GuessingGame.models;

namespace GuessingGame.Models;

public class ScoreboardLine
{
    public int Id { get; set; }
    public virtual Player Player { get; set; }
    public int Score { get; set; }
    public virtual GuessingGameModel Game { get; set; }
}