using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class PlayerAnswerProxy: PlayerAnswer
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IAnswerRepository _answerRepository;

    public PlayerAnswerProxy(IQuestionRepository questionRepository, IPlayerRepository playerRepository, IAnswerRepository answerRepository)
    {
        _questionRepository = questionRepository;
        _playerRepository = playerRepository;
        _answerRepository = answerRepository;
    }

    public override QuizGameQuestion? QuizGameQuestion
    {
        get
        {
            if (base.QuizGameQuestion == null)
            {
                base.QuizGameQuestion = _questionRepository.GetQuizGameQuestionByPlayerAnswerId(Id);
            }

            return base.QuizGameQuestion;
        }
    }

    public override Player? Player
    {
        get
        {
            if (base.Player == null)
            {
                base.Player = _playerRepository.GetPlayerByQuizGameAnswerId(Id);
            }
            
            return base.Player;
        }
        
        set => base.Player = value;
    }
    
    public override Answer? Answer
    {
        get
        {
            if (base.Answer == null)
            {
                base.Answer = _answerRepository.GetAnswerByQuizGameAnswerId(Id);
            }
            
            return base.Answer;
        }
        
        set => base.Answer = value;
    }
}