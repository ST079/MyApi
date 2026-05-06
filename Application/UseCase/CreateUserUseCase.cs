using MyApi.Application.DTOs;
using MyApi.Models;
using MyApi.Repository;
using BCrypt.Net;

namespace MyApi.Application.UseCases.CreateUser;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Execute(CreateUserInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Name))
            throw new Exception("Name is required");

        if (string.IsNullOrWhiteSpace(input.Email))
            throw new Exception("Email is required");

        if (string.IsNullOrWhiteSpace(input.Password))
            throw new Exception("Password is required");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(input.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Email = input.Email,
            Password = hashedPassword,
            Phone = input.Phone ?? string.Empty,
            Address = input.Address ?? string.Empty,
        };

        return await _userRepository.CreateUser(user);
    }
}