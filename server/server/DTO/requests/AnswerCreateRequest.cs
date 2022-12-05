// Rewrite as a class
namespace GuessingGame.DTO.requests;

public class AnswerCreateRequest
{
    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
}