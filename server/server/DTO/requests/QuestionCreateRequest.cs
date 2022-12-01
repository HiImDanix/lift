namespace GuessingGame.DTO.requests;
public record QuestionCreateRequest
(
    string ImagePath,
    string QuestionText,
    string Category,
    List<AnswerCreateRequest> Answers
);
