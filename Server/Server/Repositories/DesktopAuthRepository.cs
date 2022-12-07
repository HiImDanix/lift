using System.Data;
using Dapper;
using GuessingGame.Exceptions;
using GuessingGame.models;
using GuessingGame.Models;

namespace GuessingGame.Repositories;

public class DesktopAuthRepository : IDesktopAuthRepository
{
    private readonly IDbConnection _db;

    public DesktopAuthRepository(IDbConnection db)
    {
        _db = db;
    }
/*
    public Player? Get(int id)
    {
        var sql = "SELECT * FROM Players WHERE ID = @id";
        return _db.QueryFirstOrDefault<Player>(sql, new { id });
    }
*/
    public Administrator? GetByEmail(string email)
    {
        var sql = "SELECT * FROM Administrators WHERE email = @email";
        return _db.QueryFirstOrDefault<Administrator>(sql, new {email});
    }
}