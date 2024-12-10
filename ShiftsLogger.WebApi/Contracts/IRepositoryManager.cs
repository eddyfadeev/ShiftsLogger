using Contracts.Repository;

namespace Contracts;

public interface IRepositoryManager
{
    ILocationRepository Location { get; }
    IUserRepository User { get; }
    IShiftTypeRepository ShiftType { get; }
    IShiftRepository Shift { get; }
    Task SaveAsync();
}