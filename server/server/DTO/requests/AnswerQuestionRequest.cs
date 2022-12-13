using System.ComponentModel.DataAnnotations;

namespace GuessingGame.DTO.requests;

public class AnswerQuestionRequest
{
    public int GameQuestionId { get; set; }
    // TODO: Improve: use answerId instead of answerText
    public string Answer { get; set; }
}