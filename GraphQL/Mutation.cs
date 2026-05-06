using MyApi.Models;
using MyApi.Services;
using MyApi.Application.DTOs;
using MyApi.Application.UseCases.CreateUser;
namespace MyApi.GraphQL;

public class Mutation
{
    // Placeholder for future mutations
    public async Task<User> CreateUser
    (CreateUserInput createUserInput, [Service] CreateUserUseCase createUserUseCase)
    {
        User newUser = new User
        {
            Name = createUserInput.Name,
            Email = createUserInput.Email,
            Password = createUserInput.Password,
            Phone = createUserInput.Phone! ,
            Address = createUserInput.Address!,
        };
        return await createUserUseCase.Execute(createUserInput);
    }

    public async Task<User> UpdateUser
    (UpdateUserInput input, [Service] UserService userService)
    {
        User updatedUser = new User { Id = input.Id, Name = input.Name!, Email = input.Email!, Phone = input.Phone!, Address = input.Address! };
        return await userService.UpdateUser(updatedUser);
    }

    public async Task<bool> DeleteUser(Guid id, [Service] UserService userService)
    {
        return await userService.DeleteUser(id);
    }
}