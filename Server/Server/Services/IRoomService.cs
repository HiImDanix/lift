using GuessingGame.DTO.responses;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface IRoomService
{
    public PlayerPrivateWithRoomDTO CreateRoom(string playerDisplayName);
}