using GuessingGame.dto;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface IPlayerService
{
    PrivatePlayerResponse? GetPlayer(int id);
}