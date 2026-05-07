using MyApi.Application.DTOs;
using MyApi.Models;
using MyApi.Infrastructure.Repository;
using FluentValidation;
using MyApi.utils;
using MyApi.Infrastructure.UnitOfWork;

namespace MyApi.Application.UseCases.CreateUser;

public class CreateUserUseCase
{
    private readonly IValidator<CreateUserInput> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserUseCase(IUserRepository userRepository, IValidator<CreateUserInput> validator, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Execute(CreateUserInput input)
    {
        var result = await _validator.ValidateAsync(input);

        if (!result.IsValid)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        }

        var hashedPassword = BCryptPassword.HashPassword(input.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Email = input.Email,
            Password = hashedPassword,
            Phone = input.Phone ?? string.Empty,
            Address = input.Address ?? string.Empty,
        };

        await _userRepository.CreateUser(user);
        
        await _unitOfWork.SaveChangesAsync();
        return user;
    }
}