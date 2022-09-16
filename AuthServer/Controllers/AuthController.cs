using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using AuthServer.Models;
using AuthServer.Repositories;
using AuthServer.Services;

namespace AuthServer.Controllers;

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
    public ActionResult<LoginResponse> Login(LoginRequest loginRequest)
    {
        var username = loginRequest.Username;
        var password = loginRequest.Password;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return Unauthorized("Invalid credentials");
        }

        var validUser = GetUser(username, password);

        if (validUser != null)
        {
            var generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
            if (generatedToken != null)
            {
                Response.Headers.Add("Authorization", "Bearer " + generatedToken);
                return new LoginResponse(generatedToken, new UserDTO(validUser));
            }
        }

        return Unauthorized("Invalid credentials");
    }
    
    private User GetUser(String username, String password)
    {
        // Write your code here to authenticate the user     
        return _userRepository.GetUser(username, password);
    }
}