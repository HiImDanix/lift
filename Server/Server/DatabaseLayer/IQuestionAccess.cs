using Server.Models;

namespace Server.DatabaseLayer
{
    public interface IQuestionAccess
    {
        int CreateQuestion(Question question);
    }
}
