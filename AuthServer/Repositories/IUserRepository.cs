using OLHBackend.Models;

namespace OLHBackend.Repositories;

public interface IUserRepository
{
    User GetUser(string username, string password);
}