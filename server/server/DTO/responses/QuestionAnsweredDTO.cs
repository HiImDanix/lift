namespace GuessingGame.DTO.responses;

public class QuestionAnsweredDTO
{
    public PlayerPublicDTO player { get; set; }
    public int answerId { get; set; }
}