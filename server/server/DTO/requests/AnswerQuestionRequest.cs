using System.ComponentModel.DataAnnotations;

namespace GuessingGame.DTO.requests;

public class AnswerQuestionRequest
{
    [Required]
    public int GameQuestionId { get; set; }
    // TODO: Improve: use answerId instead of answerText
    [Required]
    public string Answer { get; set; }
}