namespace MyApi.Application.DTOs;

public class CreateUserInput
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public string? Phone { get; set; } 
    public string? Address { get; set; } 
}