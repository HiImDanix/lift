using GuessingGame.models;

namespace GuessingGame.Repositories;

public interface IRoomRepository
{
    public Room Create(Room room);
    void AddPlayer(Room room, Player player);
}