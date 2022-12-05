using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class AnswerProxy: Answer
{
    private readonly IQuestionRepository _questionRepository;
    
    public AnswerProxy(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    
    
    public override Question? Question
    {
        get
        {
            if (base.Question == null)
            {
                base.Question = _questionRepository.GetQuestionByAnswerId(Id);
            }
            
            return base.Question;
        }
        
        set => base.Question = value;
    }
}