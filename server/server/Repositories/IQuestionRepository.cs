using GuessingGame.models;
using GuessingGame.Models;

namespace GuessingGame.Repositories
{
    public interface IQuestionRepository
    {
        public Question? Get(int id);

    }
}
