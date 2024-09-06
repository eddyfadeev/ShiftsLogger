using ShiftsLogger.Application.Interfaces.Data.Operations;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Data.Repositories;

public interface ILocationsRepository :
    IAddToRepository<Location>,
    IGetFromRepository<Location>,
    IUpdateInRepository<Location>,
    IRemoveFromRepository<Location>
{
    List<Location> GetAllLocations();
}