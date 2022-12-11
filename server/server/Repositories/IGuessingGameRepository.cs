using GuessingGame.Models;

namespace GuessingGame.Repositories;

public interface IGuessingGameRepository
{
    public GuessingGameModel Add(GuessingGameModel game);
    GuessingGameModel GetByRoomId(int id);
}