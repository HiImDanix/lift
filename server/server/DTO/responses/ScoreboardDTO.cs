using GuessingGame.models;
using GuessingGame.Models;

namespace GuessingGame.DTO.responses;

public class ScoreboardDTO
{
    public List<ScoreboardLineDTO> Scores { get; set; }
}