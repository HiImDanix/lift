using Microsoft.Data.SqlClient;

namespace GuessingGame;

public interface IDbHelper
{
    public void StartTransaction();
    public void CommitTransaction();
    public void RollbackTransaction();
}

public class DbHelper: IDbHelper
{
    private readonly string _connString;
    
    public DbHelper(string connString)
    {
        _connString = connString;
    }
    
    // start transaction
    public void StartTransaction()
    {
        using (var conn = new SqlConnection(_connString))
        {
            conn.Open();
            using (var cmd = new SqlCommand("BEGIN TRANSACTION", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    
    // commit transaction
    public void CommitTransaction()
    {
        using (var conn = new SqlConnection(_connString))
        {
            conn.Open();
            using (var cmd = new SqlCommand("COMMIT TRANSACTION", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    
    // rollback transaction
    public void RollbackTransaction()
    {
        using (var conn = new SqlConnection(_connString))
        {
            conn.Open();
            using (var cmd = new SqlCommand("ROLLBACK TRANSACTION", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}