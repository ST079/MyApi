using MyApi.Models;

namespace MyApi.Infrastructure.Repository;

    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUserById(Guid id);
        Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(Guid id);
        Task<User>Login(string email, string password);
    }