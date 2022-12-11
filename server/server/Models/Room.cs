using System.ComponentModel.DataAnnotations;
using GuessingGame.Models;

namespace GuessingGame.models;

public class Room
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    // Virtual to allow proxy objects to override the property behavior
    public virtual IList<Player>? Players { get; set; }
    public virtual Player? Host { get; set; }
    public int? HostId {
        get
        {
            return this.Host?.Id;
        }
    }
    public long StartTime { get; set; }
    public virtual GuessingGameModel? CurrentGame { get; set; }
}