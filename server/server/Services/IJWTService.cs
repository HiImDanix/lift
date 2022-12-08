using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GuessingGame.models;
using GuessingGame.Models;
using Microsoft.IdentityModel.Tokens;

namespace GuessingGame.Services;

public interface IJWTService
{

    public string GenerateJwtToken(Player player);
    public string GenerateJwtToken(Administrator admin);

}