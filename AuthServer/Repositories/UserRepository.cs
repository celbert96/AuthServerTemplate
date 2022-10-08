using AuthServer.Models;
using AuthServer.Utils;

namespace AuthServer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User?> _users = new List<User?>();
    private readonly IDatabaseUtil _databaseUtil;

    private List<UserRoles> _defaultRoles = new()
    {
        UserRoles.User
    };
    
    public UserRepository(IDatabaseUtil databaseUtil)
    {
        _databaseUtil = databaseUtil;
        
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
    
    public User? GetUser(string username, string password)
    {
        var bindVars = new Dictionary<string, object>
        {
            {"username", username},
            {"password", password}
        };
        
        var query = "SELECT U.ID, U.USERNAME, U.CREATED_DATE, R.ROLE_ID FROM USERS U LEFT JOIN USER_ROLES R ON U.ID = R.USER_ID WHERE U.USERNAME = @username AND U.PASSWORD = @password;";
        var res = _databaseUtil.PerformQuery(query, bindVars);

        return res.Count == 0 ? null : User.CreateFromDatabaseQueryResult(res);
    }
}