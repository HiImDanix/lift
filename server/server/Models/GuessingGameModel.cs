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
    public Room room { get; set; }
    public long startTime { get; set; }
    public int totalRounds { get; set; }
    public int roundDurationMs { get; set; }
    public int scoreboardDurationMs { get; set; }
    public int currentRound { get; set; } = 1;
    public long currentRoundStartTime { get; set; }
    public string Status { get; set; } = GameStatus.Instructions.ToString();
    public string GameType { get; set; } = "GuessingGame";

    public GuessingGameModel(Room room, long startTime, int totalRounds, int roundDurationMs, int scoreboardDurationMs)
    {
        this.room = room;
        this.startTime = startTime;
        this.totalRounds = totalRounds;
        this.roundDurationMs = roundDurationMs;
        this.scoreboardDurationMs = scoreboardDurationMs;
    }
    
    public GuessingGameModel()
    {
        
    }
}