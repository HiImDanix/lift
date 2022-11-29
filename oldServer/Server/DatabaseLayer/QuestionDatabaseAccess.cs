using Server.Models;
using System.Data.SqlClient;

namespace Server.DatabaseLayer
{
    public class QuestionDatabaseAccess : IQuestionAccess
    {
        readonly string _connectionString;

        public QuestionDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LIFTConnection");
        }

        // For test
        public QuestionDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }
        public int CreateQuestion(Question aQuestion)
        {
            int insertedId = -1;
            //

            string insertString = "insert into Question(imgpath, questionText, category, answer) OUTPUT INSERTED.ID values(@imgpath, @questionText, @category, @answer)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))

            {
                // Prepace SQL
                SqlParameter imgpath = new("@imgpath", aQuestion.ImagePath);
                CreateCommand.Parameters.Add(imgpath);
                SqlParameter questionText = new("@questionText", aQuestion.QuestionText);
                CreateCommand.Parameters.Add(questionText);
                SqlParameter category = new("@category", aQuestion.Category);
                CreateCommand.Parameters.Add(category);
                SqlParameter answer = new("@answer", aQuestion.Answer);
                CreateCommand.Parameters.Add(answer);
                //
                con.Open();
                //Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();    

            }
            return insertedId;
            
        }
    }

}
