using System.Data;
using Microsoft.Data.SqlClient;

namespace GuessingGame;

public interface IDbHelper
{
    public void StartTransaction();
    public void CommitTransaction();
    public void RollbackTransaction();
}

public class DbHelper : IDbHelper
{
    private readonly IDbConnection _db;

    // public DbHelper(IDbConnection db)
    // {
    //     _db = db;
    // }

    public void StartTransaction()
    {
        // using var cmd = _db.CreateCommand();
        // cmd.CommandText = "BEGIN TRANSACTION";
        // cmd.ExecuteNonQuery();
    
    }
    
    public void CommitTransaction()
    {
        // using var cmd = _db.CreateCommand();
        // cmd.CommandText = "COMMIT TRANSACTION";
        // cmd.ExecuteNonQuery();
    }
    
    public void RollbackTransaction()
    {
    //     using var cmd = _db.CreateCommand();
    //     cmd.CommandText = "ROLLBACK TRANSACTION";
    //     cmd.ExecuteNonQuery();
    }
}