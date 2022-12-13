namespace GuessingGame.DTO.responses;

public class RoundStartDto
{
    public int Id { get; set; }
    public int CurrentRound { get; set; }
    public long CurrentRoundStartTime { get; set; }
    public string Status { get; set; }
    public QuestionDTO CurrentQuestion { get; set; }
    public int? CurrentQuizGameQuestionId { get; set; }
}