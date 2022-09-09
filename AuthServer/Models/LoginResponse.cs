namespace AuthServer.Models;

public class LoginResponse
{ 
    public string Token { get; }
    public UserDTO User { get; }

    public LoginResponse(string token, UserDTO user)
    {
        this.Token = token;
        this.User = user;
    }

    
}