using GuessingGame.models;

namespace GuessingGame.Repositories;

public interface IRoomRepository
{
    public Room Create(Room room);
    public Player AddPlayer(Room room, Player player, bool checkIfGameStarted = true);
    Room GetRoomById(object roomId);
    Room? GetByCode(string roomCode);
    Room? Get(int id);
    Room SetHost(Room room, Player player);
    Room UpdateStartTime(Room lobby, long startTime);
}