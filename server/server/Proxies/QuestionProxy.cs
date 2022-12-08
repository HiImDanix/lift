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
            return _answerRepository.GetAnswersForQuestion(base.Id);
        }
    }
}