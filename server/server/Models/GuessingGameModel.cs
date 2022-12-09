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
    public Room Room { get; set; }
    public long StartTime { get; set; }
    public int TotalRounds { get; set; }
    public int RoundDurationMs { get; set; }
    public int ScoreboardDurationMs { get; set; }
    public int CurrentRound { get; set; } = 1;
    public long CurrentRoundStartTime { get; set; }
    public string Status { get; set; } = GameStatus.Instructions.ToString();
    public string GameType { get; set; } = "GuessingGame";

    public GuessingGameModel(Room room, long startTime, int totalRounds, int roundDurationMs, int scoreboardDurationMs)
    {
        this.Room = room;
        this.StartTime = startTime;
        this.TotalRounds = totalRounds;
        this.RoundDurationMs = roundDurationMs;
        this.ScoreboardDurationMs = scoreboardDurationMs;
    }
    
    public GuessingGameModel()
    {
        
    }
}