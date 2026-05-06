using FluentValidation;
using MyApi.Application.DTOs;
using MyApi.Models;
using MyApi.Repository;

namespace MyApi.Application.UseCases.UpdateUser;

public class UpdateUserUseCase
{
    private readonly  IValidator<UpdateUserInput> _validator;
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

        if (!string.IsNullOrWhiteSpace(input.Name))
            user.Name = input.Name;

        if (!string.IsNullOrWhiteSpace(input.Email))
            user.Email = input.Email;

        if (input.Phone != null)
            user.Phone = input.Phone;

        if (input.Address != null)
            user.Address = input.Address;

        return await _userRepository.UpdateUser(user);
    }
}