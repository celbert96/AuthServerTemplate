using AuthServer.Models;

namespace AuthServer.Repositories;

public interface IUserRepository
{
    User GetUser(string username, string password);
}