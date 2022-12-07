using GuessingGame.DTO.responses;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface IDesktopAuthService
{
    bool Login(string email, string password);
}