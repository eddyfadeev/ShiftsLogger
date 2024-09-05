using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces;

public interface IShiftsLoggerService
{
    List<Shift> GetAllShifts();
}