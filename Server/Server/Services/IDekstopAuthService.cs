using GuessingGame.DTO.responses;
using GuessingGame.models;

namespace GuessingGame.Services;

public interface IDesktopAuthService
{
    AdministratorDTO? Login(string email, string password);
}