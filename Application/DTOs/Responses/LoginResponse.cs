
using MyApi.Application.DTOs.Inputs;
namespace MyApi.Application.DTOs.Responses;

public class LoginResponse
{
    public string Token { get; set; } = default!;
    public UserResponse LoggedInUser { get; set; } = default!;
}