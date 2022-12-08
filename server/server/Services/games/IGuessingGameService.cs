using GuessingGame.Models;

namespace GuessingGame.Services;

public interface IGuessingGameService
{
    public void StartGame(GuessingGameModel model);
}