namespace GuessingGame.DTO.responses;

public class ScoreboardLineDTO
{
    public int Position { get; set; }
    public PlayerPublicDTO Player { get; set; }
    public int Score { get; set; }
}