using GuessingGame.models;

namespace GuessingGame.Repositories;

public interface IRoomRepository
{
    public Room Create(Room room);
    public Player AddPlayer(Room room, Player player);
}