using System.ComponentModel.DataAnnotations;

namespace GuessingGame.models;

public class Room
{

    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    
    public enum Relations
    {
        CurrentGame,
        Players
    }

    public Room(string code)
    {
        Code = code;
    }

}