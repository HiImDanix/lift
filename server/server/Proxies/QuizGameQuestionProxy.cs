using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class QuizGameQuestionproxy: QuizGameQuestion
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IGuessingGameRepository _guessingGameRepository;
    private readonly IPlayerAnswersRepository _playerAnswersRepository;
    
    public QuizGameQuestionproxy(IQuestionRepository questionRepository, IGuessingGameRepository guessingGameRepository, IPlayerAnswersRepository playerAnswersRepository)
    {
        _questionRepository = questionRepository;
        _guessingGameRepository = guessingGameRepository;
        _playerAnswersRepository = playerAnswersRepository;
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

    public override List<PlayerAnswer>? Answers
    {
            get
        {
            if (base.Answers == null)
            {
                base.Answers = _playerAnswersRepository.GetPlayerAnswersByQuizGameQuestionId(base.Id);
            }
            
            return base.Answers;
        }
        
        set
        {
            base.Answers = value;
        }
    }
}