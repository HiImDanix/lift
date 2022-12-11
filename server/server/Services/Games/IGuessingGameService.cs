using GuessingGame.Models;

namespace GuessingGame.Services;

public interface IGuessingGameService
{
    public GuessingGameModel StartGame(GuessingGameModel model);
}