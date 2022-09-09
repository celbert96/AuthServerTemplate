using Microsoft.AspNetCore.Mvc;
using OLHBackend.Models;
using OLHBackend.Repositories;
using OLHBackend.Services;

namespace OLHBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(IConfiguration config, ITokenService tokenService, IUserRepository userRepository, ILogger<AuthController> logger)
    {
        _config = config;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _logger = logger;
    }
    
    [HttpPost(Name = "Login")]
    public string Login(String username, String password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return "Error - null or empty";
        }

        var validUser = GetUser(username, password);

        if (validUser != null)
        {
            var generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
            if (generatedToken != null)
            {
                return generatedToken;
            }
        }

        return "Failure";
    }
    
    private User GetUser(String username, String password)
    {
        // Write your code here to authenticate the user     
        return _userRepository.GetUser(username, password);
    }
}