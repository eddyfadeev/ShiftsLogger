using ShiftsLogger.Application.Interfaces.Data.Operations;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Data.Repositories;

public interface IUsersRepository :
    IAddToRepository<User>,
    IGetFromRepository<User>,
    IUpdateInRepository<User>,
    IRemoveFromRepository<User>
{
    List<User> GetAllUsers();
}