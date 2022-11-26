using System.ComponentModel.DataAnnotations;

namespace GuessingGame.models;

public class Room
{

    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    
    public IEnumerable<int> Players { get; set; } = new HashSet<int>();

    public Room(string code)
    {
        Code = code;
    }

}