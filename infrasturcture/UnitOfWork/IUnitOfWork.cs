using MyApi.Infrastructure.Repository;

namespace MyApi.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    Task SaveChangesAsync();
}