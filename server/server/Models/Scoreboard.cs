using GuessingGame.models;

namespace GuessingGame.Models;

public class ScoreboardLine
{
    public int Id { get; set; }
    public int Position { get; set; }
    public virtual Player Player { get; set; }
    public virtual int Score { get; set; }
    public GuessingGameModel Game { get; set; }
}