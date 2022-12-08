using System.ComponentModel.DataAnnotations;

namespace GuessingGame.DTO.requests;
public class QuestionCreateRequest
{
    [Required]
    [Url]
    
    public string ImagePath { get; set; }
    [Required]
    public string QuestionText { get; set; }
    [Required]
    public string Category { get; set; }
    public byte[] RowVer { get; set; }
    [Required]
    public List<AnswerNoId> Answers { get; set; }
}
