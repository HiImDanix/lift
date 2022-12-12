using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class QuizGameQuestionproxy: QuizGameQuestion
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IGuessingGameRepository _guessingGameRepository;
    
    public QuizGameQuestionproxy(IQuestionRepository questionRepository, IGuessingGameRepository guessingGameRepository)
    {
        _questionRepository = questionRepository;
        _guessingGameRepository = guessingGameRepository;
    }
    
    public override Question Question
    {
        get
        {
            if (base.Question == null)
            {
                base.Question = _questionRepository.GetQuestionByQuizGameQuestionId(base.Id);
            }
            
            return base.Question;
        }
        
        set
        {
            base.Question = value;
        }
    }
    
    public override GuessingGameModel Game
    {
        get
        {
            if (base.Game == null)
            {
                base.Game = _guessingGameRepository.GetGuessingGameByQuizGameQuestionId(base.Id);
            }
            
            return base.Game;
        }
        
        set
        {
            base.Game = value;
        }
    }
}