using System.ComponentModel.DataAnnotations;


namespace MyApi.Models;

public class User
{
    public Guid Id { get; set; } =Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    public string[] Roles { get; set; } = ["User"];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
