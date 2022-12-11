using GuessingGame.DTO.responses;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface ILobbyService
{
    public LoginWithRoomDTO CreateRoomAndPlayer(string playerDisplayName);
    LoginWithRoomDTO JoinLobby(string roomCode, string playerDisplayName);
    RoomDTO GetLobby(int lobbyId);
    GameDTO StartGame(int lobbyId);
}