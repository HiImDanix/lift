using GuessingGame.models;
using GuessingGame.Models;

namespace GuessingGame.Repositories;

public interface IRoomRepository
{
    public Room Create(Room room);
    public Player AddPlayer(Room room, Player player);
    Room GetRoomById(object roomId);
    Room? GetByCode(string roomCode);
    Room? Get(int id);
    Room SetHost(Room room, Player player);
    Room UpdateStartTime(Room lobby, long startTime);
    void UpdateCurrentGame(Room room, GuessingGameModel model);
    Room? GetRoomByGuessingGameId(int id);
    QuizGameQuestion? GetQuizGameQuestionByGuessingGameId(int id);
    List<QuizGameQuestion> GetQuizGameQuestionsByGuessingGameId(int id);
    Room GetRoomByPlayerId(int id);
}