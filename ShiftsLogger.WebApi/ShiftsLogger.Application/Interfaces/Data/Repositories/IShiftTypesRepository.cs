using ShiftsLogger.Application.Interfaces.Data.Operations;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Data.Repositories;

public interface IShiftTypesRepository :
    IAddToRepository<ShiftType>,
    IGetFromRepository<ShiftType>,
    IUpdateInRepository<ShiftType>,
    IRemoveFromRepository<ShiftType>
{
    List<ShiftType> GetAllShiftTypes();
}