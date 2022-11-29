using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GuessingGame.models;

public class Player
{
    public int Id { get; set; }
    [Required]
    public string Session { get; set; }
    [MinLength(3),
     MaxLength(16),
     Required,
     RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Player name can only contain letters and numbers")]
    public string DisplayName { get; set; }
    [JsonIgnore]
    public virtual Room? Room { get; set; }

}