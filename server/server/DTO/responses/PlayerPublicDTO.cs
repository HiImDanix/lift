namespace GuessingGame.DTO.responses;

// Note: Automapper does not work when it is a record; No constructor error.
public class PlayerPublicDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
}