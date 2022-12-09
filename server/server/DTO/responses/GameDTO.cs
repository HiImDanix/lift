using System.Text.Json.Serialization;

namespace GuessingGame.DTO.responses;

public class GameDTO
{

    public long StartTime { get; set; }
    // Deserializing enum as string does not work for some reason, so using string instead
    public string Status { get; set; }
    public string GameType { get; set; }
    public int TotalRounds { get; set; }
    public int RoundDurationMs { get; set; }
    public int ScoreboardDurationMs { get; set; }
    public int CurrentRound { get; set; }
    public long CurrentRoundStartTime { get; set; }
    // public Scoreboard scoreboard;
    public QuestionNoAnswersDTO CurrentQuestion { get; set; }
    
    
    public GameDTO(long startTime, string status, string gameType, int currentRound, int totalRounds, int roundDurationMs, int scoreboardDurationMs, long currentRoundStartTime)
    {
        StartTime = startTime;
        Status = status;
        GameType = gameType;
        CurrentRound = currentRound;
        TotalRounds = totalRounds;
        RoundDurationMs = roundDurationMs;
        ScoreboardDurationMs = scoreboardDurationMs;
        CurrentRoundStartTime = currentRoundStartTime;
    }
    
    public GameDTO()
    {
    }
}