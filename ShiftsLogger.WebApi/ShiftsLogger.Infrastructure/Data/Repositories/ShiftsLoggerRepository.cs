using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Infrastructure.Data.Repositories;

public class ShiftsLoggerRepository : IShiftsLoggerRepository
{
    public List<Shift> GetAllShifts()
    {
        throw new NotImplementedException();
    }
}