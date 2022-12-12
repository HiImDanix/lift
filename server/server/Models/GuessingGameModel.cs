using GuessingGame.DTO.responses;
using GuessingGame.models;

namespace GuessingGame.Models;


public enum GameStatus
{
    Instructions,
    Playing,
    Scoreboard,
    Finished
}

public class GuessingGameModel
{
    public int Id { get; set; }
    public virtual Room? Room { get; set; }
    public long? StartTime { get; set; }
    public int TotalRounds { get; set; }
    public int RoundDurationMs => 5000;
    public int ScoreboardDurationMs => 5000;
    public int? CurrentRound { get; set; }
    public long? CurrentRoundStartTime { get; set; }
    public string Status { get; set; }
    public string GameType { get; set; } = "GuessingGame";
    public virtual QuizGameQuestion? CurrentQuizGameQuestion { get; set; }
    public int? currentQuizGameQuestionID { 
        get {
            return CurrentQuizGameQuestion?.Id;
        }
    }
    
    public virtual List<QuizGameQuestion>? Questions { get; set; }

    public GuessingGameModel(Room room, long startTime, int totalRounds)
    {
        this.Room = room;
        this.StartTime = startTime;
        this.TotalRounds = totalRounds;
        this.Status = GameStatus.Instructions.ToString();
        this.CurrentRound = 1;
    }
    
    public GuessingGameModel()
    {
        
    }
    
}