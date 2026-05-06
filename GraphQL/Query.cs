using MyApi.Models;
using MyApi.Services;
namespace MyApi.GraphQL;

public class Query
{
    // public string Hello() => "Hello, World!";

    public async Task<List<User>> GetUsers([Service] UserService userService)
    {
       return await userService.GetUsers();
    }


    public async Task<User?> GetUserById(Guid id, [Service] UserService userService)
    {
        return await userService.GetUserById(id);
    }

    public async Task<User> GetUserByEmail(string email, [Service] UserService userService)
    {
        return await userService.GetUserByEmail(email);
    }

    
}