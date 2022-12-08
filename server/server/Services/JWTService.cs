using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GuessingGame.models;
using GuessingGame.Models;
using Microsoft.IdentityModel.Tokens;

namespace GuessingGame.Services;

public class JWTService: IJWTService
{
    
    private readonly IConfiguration _configuration;
    
    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateJwtToken(Player player)
    {
        // JWT
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // claims
        var claims = new List<Claim>
        {
            // id
            new("sub", player.Id.ToString()),
            new("name", player.DisplayName),
            new("roomID", player.Room.Id.ToString()),
            // Role
            new(ClaimTypes.Role, "Player"),
        };
        
        var token = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GenerateJwtToken(Administrator administrator)
    {
        // JWT
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // claims
        var claims = new List<Claim>
        {
            // id
            new("sub", administrator.Id.ToString()),
            new("email", administrator.Email),
            // Role
            new(ClaimTypes.Role, "Administrator"),
        };
        
        var token = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}