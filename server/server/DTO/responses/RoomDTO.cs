namespace GuessingGame.DTO.responses;

// Note: Automapper does not work when it is a record; No constructor error.
public class RoomDTO
{
    public int Id { get; init; }
    public string Code { get; init; }
    public IEnumerable<PlayerPublicDTO> Players { get; init; }
}