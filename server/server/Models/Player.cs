namespace GuessingGame.models;

public class Player
{
    public int Id { get; set; }
    public string Session { get; set; }
    public string DisplayName { get; set; }
    public int RoomId { get; set; }
    
    public Player(int id, string session, string displayName)
    {
        Id = id;
        Session = session;
        DisplayName = displayName;
    }

    public Player(string session, string displayName)
    {
        Session = session;
        DisplayName = displayName;
    }
    
}