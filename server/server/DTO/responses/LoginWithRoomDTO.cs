using GuessingGame.DTO.responses;
using GuessingGame.Models;

namespace GuessingGame.DTO.responses;

// Note: Automapper does not work when it is a record; No constructor error.
public class LoginWithRoomDTO
{
    public int Id { get; init; }
    public string Session { get; init; }
    public string Name { get; init; }
    public RoomDTO Room { get; init; }
    public GuessingGameModel? CurrentGame { get; set; }
}