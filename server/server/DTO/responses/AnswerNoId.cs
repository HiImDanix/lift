// Rewrite as a class

using System.ComponentModel.DataAnnotations;

namespace GuessingGame.DTO.requests;

public class AnswerNoId
{
    [Required]
    public string AnswerText { get; set; }
    [Required]
    public bool IsCorrect { get; set; }
}