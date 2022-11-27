using GuessingGame.models;

namespace GuessingGame.Repositories;

public interface IRoomRepository
{
    public Room Create(Room room);
    public Room AddPlayer(Room room, Player player);
    Room GetRoomById(object roomId);
}