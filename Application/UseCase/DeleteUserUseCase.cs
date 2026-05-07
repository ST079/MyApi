using MyApi.Infrastructure.Repository;
using MyApi.Infrastructure.UnitOfWork;
using MyApi.Models;

namespace MyApi.Application.UseCases.DeleteUser;

public class DeleteUserUseCase
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Execute(Guid id)
    {
        if (id == Guid.Empty) throw new Exception("Invalid Operation, Id is required");

        var result = await _userRepository.DeleteUser(id);
        if (result)
        {
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }
}