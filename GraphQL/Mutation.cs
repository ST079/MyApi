using MyApi.Models;
using MyApi.Services;

namespace MyApi.GraphQL;

public class Mutation
{
    // Placeholder for future mutations
    public async Task<User> CreateUser(string name, string email, string password, string phone, string address, [Service] UserService userService)
    {
        User newUser = new User { Id = Guid.NewGuid(), Name = name, Email = email, Password = password, Phone = phone, Address = address };
        return await userService.CreateUser(newUser);
    }

    public async Task<User> UpdateUser
    (UpdateUserInput input, [Service] UserService userService)
    {
        User updatedUser = new User { Id = input.Id, Name = input.Name ?? string.Empty, Email = input.Email ?? string.Empty, Phone = input.Phone ?? string.Empty, Address = input.Address ?? string.Empty };
        return await userService.UpdateUser(updatedUser);
    }

    public async Task<bool> DeleteUser(Guid id, [Service] UserService userService)
    {
        return await userService.DeleteUser(id);
    }
}