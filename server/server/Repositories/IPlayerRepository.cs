using System.Linq.Expressions;
using GuessingGame.models;

namespace GuessingGame.Repositories;

public interface IPlayerRepository
{
    public Player Create(Player player);
    public Player? Get(int id);
}