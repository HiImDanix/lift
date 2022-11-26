using GuessingGame.models;

namespace GuessingGame.Services;

public interface IRoomService
{
    public Room CreateRoom(string playerDisplayName);
}