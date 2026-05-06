using MyApi.Models;
using MyApi.infrastructure;
namespace MyApi.GraphQL;

public class Mutation
{
    // Placeholder for future mutations
    public async Task<User> CreateUser(string name, string email, string password, string phone, string address, [Service] AppDbContext dbContext)
    {
        try
        {
            // Implementation for creating a new user
            User newUser = new User { Id = Guid.NewGuid(), Name = name, Email = email, Password = password, Phone = phone, Address = address };
            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();
            return newUser;
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log the error)
            throw new Exception("An error occurred while creating the user.", ex);
        }

    }

    public async Task<User> UpdateUser
    (Guid id, string? name, string? email, string? phone, string? address, [Service] AppDbContext dbContext)
    {
        try
        {
            // Implementation for updating an existing user
            var user = await dbContext.Users.FindAsync(id);
            if (name != null) user?.Name = name;
            if (email != null) user?.Email = email;
            if (phone != null) user?.Phone = phone;
            if (address != null) user?.Address = address;
            await dbContext.SaveChangesAsync();
            return user!;
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log the error)
            throw new Exception("An error occurred while updating the user.", ex);
        }
    }

    public async Task<bool> DeleteUser(Guid id, [Service] AppDbContext dbContext)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user == null)
            throw new GraphQLException("User not found.");

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return true;
    }
}