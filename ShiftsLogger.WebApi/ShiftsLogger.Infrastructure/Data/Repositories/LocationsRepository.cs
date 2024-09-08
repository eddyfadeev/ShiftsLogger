using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Infrastructure.Data.Repositories;

public class LocationsRepository : ILocationsRepository
{
    private readonly ShiftsLoggerDbContext _context;

    public LocationsRepository(ShiftsLoggerDbContext context)
    {
        _context = context;
    }
    public int Add(Location entity)
    {
        _context.Locations.Add(entity);
        return _context.SaveChanges();
    }

    public Location? Get(int entityId) =>
        _context.Locations.FirstOrDefault(l => l.Id == entityId);

    public int Update(Location entity)
    {
        _context.Locations.Update(entity);
        return _context.SaveChanges();
    }

    public int Remove(Location entity)
    {
        _context.Locations.Remove(entity);
        return _context.SaveChanges();
    }

    public List<Location> GetAllLocations() => 
        _context.Locations.ToList();
}