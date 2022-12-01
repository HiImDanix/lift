using System.ComponentModel.DataAnnotations;

namespace GuessingGame.DTO.requests;

public record JoinRoomRequest(
    [MinLength(3),
     MaxLength(16),
     Required,
     RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Player name can only contain letters and numbers")]
    string PlayerName
);