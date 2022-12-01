namespace GuessingGame.DTO.requests;
public record QuestionCreateRequest
(
    string ImagePath,
    string QuestionText,
    string Category,
    string Answer
);
