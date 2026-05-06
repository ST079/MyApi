using MyApi.Models;
using MyApi.Repository;

namespace MyApi.Services;


public class UserService
{
    private readonly IUserRepository _userRepo;
    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _userRepo.GetUsers();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _userRepo.GetUserById(id);
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        return await _userRepo.DeleteUser(id);
    }
}