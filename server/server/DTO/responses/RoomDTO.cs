namespace GuessingGame.DTO.responses;

// Note: Automapper does not work when it is a record; No constructor error.
public class RoomDTO
{
    public int Id { get; set; }
    public string Code { get; set; }
    public IEnumerable<PlayerPublicDTO> Players { get; set; }
    public int HostId { get; set; }
}