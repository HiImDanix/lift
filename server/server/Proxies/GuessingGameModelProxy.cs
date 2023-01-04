using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class GuessingGameModelProxy: GuessingGameModel
{
    private readonly IRoomRepository _roomRepository;
    private readonly IScoreboardRepository _scoreboardRepository;
    
    public GuessingGameModelProxy(IRoomRepository roomRepository, IScoreboardRepository scoreboardRepository)
    {
        _roomRepository = roomRepository;
        _scoreboardRepository = scoreboardRepository;
    }

    public override Room? Room
    {
        get
        {
            if (base.Room == null)
            {
                base.Room = _roomRepository.GetRoomByGuessingGameId(Id);
            }
            
            return base.Room;
        }
        
        set => base.Room = value;
    }
    
    public override QuizGameQuestion? CurrentQuizGameQuestion
    {
        get
        {
            if (base.CurrentQuizGameQuestion == null)
            {
                base.CurrentQuizGameQuestion = _roomRepository.GetQuizGameQuestionByGuessingGameId(Id);
            }
            
            return base.CurrentQuizGameQuestion;
        }
        
        set => base.CurrentQuizGameQuestion = value;
    }
    
    public override List<QuizGameQuestion>? Questions
    {
        get
        {
            if (base.Questions == null)
            {
                base.Questions = _roomRepository.GetQuizGameQuestionsByGuessingGameId(Id);
            }
            
            return base.Questions;
        }
        
        set => base.Questions = value;
    }
    
    public override List<ScoreboardLine> Scoreboard
    {
        get
        {
            if (base.Scoreboard == null)
            {
                base.Scoreboard = _scoreboardRepository.GetScoreboardByGuessingGameId(Id);
            }
            
            return base.Scoreboard;
        }
        
        set => base.Scoreboard = value;
    }
}