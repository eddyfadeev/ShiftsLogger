using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Infrastructure.Data.Repositories;

public class ShiftTypesRepository : IShiftTypesRepository
{
    private readonly ShiftsLoggerDbContext _context;

    public ShiftTypesRepository(ShiftsLoggerDbContext context)
    {
        _context = context;
    }
    public int Add(ShiftType entity)
    {
        _context.ShiftTypes.Add(entity);
        return _context.SaveChanges();
    }

    public ShiftType? Get(int entityId) => 
        _context.ShiftTypes.FirstOrDefault(t => t.Id == entityId);

    public int Update(ShiftType entity)
    {
        _context.ShiftTypes.Update(entity);
        return _context.SaveChanges();
    }

    public int Remove(ShiftType entity)
    {
        _context.ShiftTypes.Remove(entity);
        return _context.SaveChanges();
    }

    public List<ShiftType> GetAllShiftTypes() =>
        _context.ShiftTypes.ToList();
}