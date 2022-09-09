using OLHBackend.Models;

namespace OLHBackend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();

    private UserRoles[] _defaultRoles = new[]
    {
        UserRoles.User
    };
    
    public UserRepository()
    {
        _users.Add(new User
        {
            Username = "user",
            Password = "password",
            CreatedDate = DateTime.Now,
            Roles = _defaultRoles
        });
        _users.Add(new User
        {
            Username = "joydipkanjilal",
            Password = "joydip123",
            CreatedDate = DateTime.Now,
            Roles = _defaultRoles
        });
        _users.Add(new User
        {
            Username = "michaelsanders",
            Password = "michael321",
            CreatedDate = DateTime.Now,
            Roles = _defaultRoles
        });
        _users.Add(new User
        {
            Username = "stephensmith",
            Password = "stephen123",
            CreatedDate = DateTime.Now,
            Roles = _defaultRoles
        });
        _users.Add(new User
        {
            Username = "rodpaddock",
            Password = "rod123",
            CreatedDate = DateTime.Now,
            Roles = _defaultRoles
        });
        _users.Add(new User
        {
            Username = "rexwills",
            Password = "rex321",
            CreatedDate = DateTime.Now,
            Roles = _defaultRoles
        });
    }
    
    public User GetUser(string username, string password)
    {
        return _users.FirstOrDefault(x => x.Username.ToLower() == username && x.Password == password);
    }
}