namespace GuessingGame.DTO.requests;
public class QuestionCreateRequest
{
    public string ImagePath { get; set; }
    public string QuestionText { get; set; }
    public string Category { get; set; }
    public byte[] RowVer { get; set; }
    public List<AnswerNoId> Answers { get; set; }
}
