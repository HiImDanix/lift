namespace GuessingGame.models;

public class Room
{
    public int Id { get; set; }
    public string Code { get; set; }
    // Virtual to allow proxy objects to override the property behavior
    public virtual IEnumerable<Player>? Players { get; set; }
    
}