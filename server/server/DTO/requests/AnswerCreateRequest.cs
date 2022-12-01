namespace GuessingGame.DTO.requests;

public record AnswerCreateRequest
{
    public string Answer;
    public bool IsCorrect;
}