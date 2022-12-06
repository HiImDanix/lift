using GuessingGame.Models;

namespace GuessingGame.Repositories;

public interface IDesktopAuthRepository
{
    Administrator? GetByEmail(string email);
}