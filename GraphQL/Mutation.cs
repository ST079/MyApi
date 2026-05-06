using MyApi.Models;
using MyApi.Services;
using MyApi.Application.DTOs;
using MyApi.Application.UseCases.CreateUser;
using MyApi.Application.UseCases.UpdateUser;
namespace MyApi.GraphQL;

public class Mutation
{
    // Placeholder for future mutations
    public async Task<User> CreateUser
    (CreateUserInput createUserInput, [Service] CreateUserUseCase createUserUseCase)
    {
        return await createUserUseCase.Execute(createUserInput);
    }

    public async Task<User> UpdateUser
    (UpdateUserInput input, [Service] UpdateUserUseCase updateUserUseCase)
    {
        User updatedUser = new User { Id = input.Id, Name = input.Name!, Email = input.Email!, Phone = input.Phone!, Address = input.Address! };
        return await updateUserUseCase.Execute(input.Id, input);
    }

    public async Task<bool> DeleteUser(Guid id, [Service] UserService userService)
    {
        return await userService.DeleteUser(id);
    }
}