using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.models;
using GuessingGame.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GuessingGame.Services;

public class DesktopAuthService : IDesktopAuthService
{

    private readonly IDesktopAuthRepository _desktopAuthRepository;
    private readonly IMapper _mapper;
    private readonly IJWTService _jwtService;

    public DesktopAuthService(IDesktopAuthRepository desktopAuthRepository, IMapper mapper, IJWTService jwtService)
    {
        _desktopAuthRepository = desktopAuthRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public AdministratorDTO? Login(string email, string password)
    {
        var administrator = _desktopAuthRepository.GetByEmail(email);
        if (administrator == null)
        {
            return null;
        }
        bool passwordCorrect = BCrypt.Net.BCrypt.Verify(password, administrator.Password);
        if(!passwordCorrect)
        {
            return null;
        }
        var adminDto = _mapper.Map<AdministratorDTO>(administrator);
        // Generate JWT token
        adminDto.Session = _jwtService.GenerateJwtToken(administrator);
        return adminDto;
    }
}