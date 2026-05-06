

using FluentValidation;
using MyApi.Application.DTOs;
using MyApi.Application.DTOs.Inputs;
using MyApi.Application.DTOs.Responses;
using MyApi.Infrastructure.Repository;
using MyApi.Services;

namespace MyApi.Application.UseCase.Login;

public class LoginUseCase
{
    private readonly IValidator<LoginInput> _validator;
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public LoginUseCase(IValidator<LoginInput> validator, IUserRepository userRepository, JwtService jwtService)
    {
        _validator = validator;
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Execute(LoginInput input)
    {
        var result = await _validator.ValidateAsync(input);

        if (!result.IsValid)
        {
            throw new Exception(
                string.Join(", ", result.Errors.Select(e => e.ErrorMessage))
            );
        }

        var user = await _userRepository.Login(input.Email, input.Password);

        if (user == null)
        {
            throw new Exception("Invalid email or password.");
        }

        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            LoggedInUser = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            },
            Token = token
        };
    }
}