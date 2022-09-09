using System.ComponentModel.DataAnnotations;

namespace OLHBackend.Models;

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
    public UserRoles[] Roles { get; set; }
}