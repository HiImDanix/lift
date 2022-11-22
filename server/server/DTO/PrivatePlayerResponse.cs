namespace GuessingGame.dto;

public record PrivatePlayerResponse(
    int ID,
    string Session,
    string DisplayName
);