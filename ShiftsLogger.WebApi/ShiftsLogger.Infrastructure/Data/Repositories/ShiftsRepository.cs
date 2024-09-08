using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Infrastructure.Data.Repositories;

public class ShiftsRepository : IShiftsRepository
{
    private readonly ShiftsLoggerDbContext _context;

    public ShiftsRepository(ShiftsLoggerDbContext context)
    {
        _context = context;
    }

    public int Add(Shift entity)
    {
        _context.Add(entity);
        return _context.SaveChanges();
    }

    public Shift? Get(int entityId) => 
        _context.Shifts.SingleOrDefault(s => s.Id == entityId);

    public int Update(Shift entity)
    {
        _context.Update(entity);
        return _context.SaveChanges();
    }

    public int Remove(Shift entity)
    {
        _context.Shifts.Remove(entity);
        return _context.SaveChanges();
    }

    public List<Shift> GetAllShifts() =>
        _context.Shifts.ToList();
}