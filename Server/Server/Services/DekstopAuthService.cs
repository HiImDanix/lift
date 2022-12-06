using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GuessingGame.DTO.responses;
using GuessingGame.models;
using GuessingGame.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace GuessingGame.Services;

public class DesktopAuthService : IDesktopAuthService
{

    private readonly IDesktopAuthRepository _desktopAuthRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public DesktopAuthService(IDesktopAuthRepository desktopAuthRepository, IMapper mapper, IConfiguration configuration)
    {
        _desktopAuthRepository = desktopAuthRepository;
        _mapper = mapper; //map the object to DTO
        _configuration = configuration;
    }



    public bool Login(string email, string password)
    {
        var administrator = _desktopAuthRepository.GetByEmail(email);
        if (administrator == null)
        {
            return false;
        }
        if (password != administrator.Password)
        {
            return false;
        }
        return true;
    }
}