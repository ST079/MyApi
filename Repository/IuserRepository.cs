using MyApi.Models;

namespace MyApi.Repository;

    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUserById(Guid id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(Guid id);
    }