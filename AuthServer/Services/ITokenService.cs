using AuthServer.Models;

namespace AuthServer.Services;

public interface ITokenService
{
    string BuildToken(string key, string issuer, User user);
    bool ValidateToken(string key, string issuer, string token);
}