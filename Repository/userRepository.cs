
using MyApi.Models;
using MyApi.infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MyApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    //constructor injection of the database context
    public UserRepository([Service] AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    //create a new user and save it to the database
    public async Task<User> CreateUser(User user)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null) throw new GraphQLException("A user with this email already exists.");
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }


    //delete a user by id
    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
            throw new GraphQLException("User not found.");

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    //fetch a user by id
    public async Task<User?> GetUserById(Guid id)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(id);
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the user.", ex);
        }
    }

    //fetch all users from the database
    public async Task<List<User>> GetUsers()
    {
        try
        {
            var user = await _dbContext.Users.ToListAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching users.", ex);
        }
    }

    //update an existing user
    public async Task<User> UpdateUser(User user)
    {
        try
        {
            var findUser = await GetUserById(user.Id);
            if (user.Name != null) findUser?.Name = user.Name;
            if (user.Email != null) findUser?.Email = user.Email;
            if (user.Phone != null) findUser?.Phone = user.Phone;
            if (user.Address != null) findUser?.Address = user.Address;
            await _dbContext.SaveChangesAsync();
            return findUser!;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the user.", ex);
        }
    }

}