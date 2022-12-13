namespace GuessingGame.DTO.responses;

public class PlayerAnswerDTO
{
    public int Id { get; set; }
    public PlayerPublicDTO Player { get; set; }
    public QuestionDTO Question { get; set; }
    public int GameQuestionId { get; set; }
    public AnswerDTO Answer { get; set; }
    public long AnsweredTime { get; set; }
}