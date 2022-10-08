using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models;

public class UserDTO
{
    [Required] 
    public string Username { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    public List<UserRoles> Roles { get; set; }

    public UserDTO(User user)
    {
        this.Username = user.Username;
        this.CreatedDate = user.CreatedDate;
        this.Roles = user.Roles;
    }
}