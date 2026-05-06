using MyApi.Models;
using MyApi.infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MyApi.GraphQL;

public class Query
{
    // public string Hello() => "Hello, World!";

    public async Task<List<User>> GetUsers([Service] AppDbContext dbContext)
    {
        try
        {
            // Implementation for fetching users from the database
            var user = await dbContext.Users.ToListAsync();
            return user;
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log the error)
            throw new Exception("An error occurred while fetching users.", ex);
        }
    }


    public async Task<User?> GetUserById(Guid id, [Service] AppDbContext dbContext)
    {
        try
        {
            // Implementation for fetching a user by ID from the database
            var user = await dbContext.Users.FindAsync(id);
            return user;
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log the error)
            throw new Exception("An error occurred while fetching the user.", ex);
        }
    }

}