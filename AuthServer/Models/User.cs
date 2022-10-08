using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models;

public class User
{
    [Required]
    public int ID { get; set; }
    
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    public List<UserRoles> Roles { get; set; }
    
    public static User CreateFromDictionary(Dictionary<string, object> dictionary)
    {
        return new User
        {
            ID = (int)dictionary["ID"],
            Username = (string)dictionary["USERNAME"],
            CreatedDate = (DateTime)dictionary["CREATED_DATE"],
            Roles = new List<UserRoles> { UserRoles.User }
        };
    }

    public static User? CreateFromDatabaseQueryResult(List<Dictionary<string, object>> queryRes)
    {
        if (queryRes.Count == 0)
        {
            return null;
        }

        var user = new User
        {
            ID = (int)queryRes[0]["ID"],
            Username = (string)queryRes[0]["USERNAME"],
            CreatedDate = (DateTime)queryRes[0]["CREATED_DATE"],
            Roles = new List<UserRoles>()
        };

        foreach (var row in queryRes)
        {
            user.Roles.Add((UserRoles)row["ROLE_ID"]);
        }

        return user;
    }
}