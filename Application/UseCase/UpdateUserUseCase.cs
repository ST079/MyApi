using MyApi.Application.DTOs;
using MyApi.Models;
using MyApi.Repository;
using System.Text.Json;

namespace MyApi.Application.UseCases.UpdateUser;

public class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public UpdateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Execute(Guid id, UpdateUserInput input)
    {
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