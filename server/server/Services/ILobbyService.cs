using GuessingGame.DTO.responses;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface ILobbyService
{
    public LobbyDTO CreateRoomAndPlayer(string playerDisplayName);
    LobbyDTO JoinLobby(string roomCode, string playerDisplayName);
    RoomDTO GetLobby(int lobbyId);
    void StartGame(int lobbyId);
}