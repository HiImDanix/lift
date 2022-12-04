using System.ComponentModel.DataAnnotations;

namespace GuessingGame.models;

public class Room
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    // Virtual to allow proxy objects to override the property behavior
    public virtual IList<Player>? Players { get; set; }
    public int HostId { get; set; }
    public virtual Player? Host { get; set; }
}