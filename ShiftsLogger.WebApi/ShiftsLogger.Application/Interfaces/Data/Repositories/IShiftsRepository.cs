using ShiftsLogger.Application.Interfaces.Data.Operations;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Data.Repositories;

public interface IShiftsRepository :
    IAddToRepository<Shift>,
    IGetFromRepository<Shift>,
    IUpdateInRepository<Shift>,
    IRemoveFromRepository<Shift>
{
    List<Shift> GetAllShifts();
}
