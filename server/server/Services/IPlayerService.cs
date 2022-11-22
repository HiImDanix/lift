using GuessingGame.dto;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface IPlayerService
{
    PrivatePlayerResponse CreatePlayer(Player player);
    PrivatePlayerResponse? GetPlayer(int id);
}