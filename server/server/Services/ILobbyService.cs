using GuessingGame.DTO.responses;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface ILobbyService
{
    public LobbyCreatedDTO CreateRoomAndPlayer(string playerDisplayName);
}