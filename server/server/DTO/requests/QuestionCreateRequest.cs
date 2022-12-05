namespace GuessingGame.DTO.requests;
public class QuestionCreateRequest
{
    public string ImagePath { get; set; }
    public string QuestionText { get; set; }
    public string Category { get; set; }
    public List<AnswerCreateRequest> Answers { get; set; }
}
