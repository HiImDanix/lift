using Server.DatabaseLayer;
using Server.Models;

namespace Server.BusinesslogicLayer
{
    public class QuestiondataControl : IQuestiondata
    {
        IQuestionAccess _questionAccess;

        public QuestiondataControl(IConfiguration inConfiguration)
        {
            _questionAccess = new QuestionDatabaseAccess(inConfiguration);
        }

        public int Add(Question question)
        {
            int insertedId;
            try
            {
                insertedId = _questionAccess.CreateQuestion(question);
            }
            catch
            {
                insertedId = -1;   
            }
            return insertedId;
        }
    }
}
