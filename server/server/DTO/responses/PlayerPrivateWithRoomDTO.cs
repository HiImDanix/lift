using GuessingGame.DTO.responses;

namespace GuessingGame.DTO.responses;

// Note: Automapper does not work when it is a record; No constructor error.
public class PlayerPrivateWithRoomDTO
{
    public int Id { get; init; }
    public string Session { get; init; }
    public string Name { get; init; }
    public RoomDTO Room { get; init; }
}