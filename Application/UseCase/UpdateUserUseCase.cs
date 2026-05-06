using FluentValidation;
using MyApi.Application.DTOs;
using MyApi.Models;
using MyApi.Infrastructure.Repository;

namespace MyApi.Application.UseCases.UpdateUser;

public class UpdateUserUseCase
{
    private readonly IValidator<UpdateUserInput> _validator;
    private readonly IUserRepository _userRepository;

    public UpdateUserUseCase(IUserRepository userRepository, IValidator<UpdateUserInput> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<User> Execute(Guid id, UpdateUserInput input)
    {
        var result = await _validator.ValidateAsync(input);

        if (!result.IsValid)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        }
        var user = await _userRepository.GetUserById(id);
        
        if (user == null)
            throw new Exception("User not found");

        return await _userRepository.UpdateUser(user);
    }
}