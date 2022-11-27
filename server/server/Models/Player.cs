using System.Text.Json.Serialization;

namespace GuessingGame.models;

public class Player
{
    public int Id { get; set; }
    public string Session { get; set; }
    public string DisplayName { get; set; }
    [JsonIgnore]
    public virtual Room? Room { get; set; }

}