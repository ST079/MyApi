

using MyApi.Infrastructure.Repository;

namespace MyApi.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _context;
    public IUserRepository Users { get; }

    public UnitOfWork(AppDbContext context, IUserRepository userRepository)
    {
        _context = context;
        Users = userRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}