using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class QuestionProxy: Question
{
    private readonly IAnswerRepository _answerRepository;
    
    public QuestionProxy(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }
    
    public override IList<Answer> Answers
    {
        get
        {
            if (base.Answers == null)
            {
                base.Answers = _answerRepository.GetAnswersForQuestion(this.Id);
            }
            
            return base.Answers;
        }
        
        set
        {
            base.Answers = value;
        }
    }
}