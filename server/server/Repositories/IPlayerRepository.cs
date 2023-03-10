using System.Linq.Expressions;
using GuessingGame.models;

namespace GuessingGame.Repositories;

public interface IPlayerRepository
{
    public Player? Get(int id);
    public IList<Player> GetPlayersByRoomId(int id);
    Player GetHostByRoomId(int id);
    Player GetPlayerByQuizGameAnswerId(int id);
    Player GetPlayerByScoreboardLineId(int id);
}