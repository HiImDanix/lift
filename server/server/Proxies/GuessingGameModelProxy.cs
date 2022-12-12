using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class GuessingGameModelProxy: GuessingGameModel
{
    private readonly IRoomRepository _roomRepository;
    
    public GuessingGameModelProxy(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
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
}