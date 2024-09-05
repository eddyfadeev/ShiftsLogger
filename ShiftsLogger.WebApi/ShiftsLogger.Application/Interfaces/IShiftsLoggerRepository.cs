using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces;

public interface IShiftsLoggerRepository
{
    List<Shift> GetAllShifts();
}