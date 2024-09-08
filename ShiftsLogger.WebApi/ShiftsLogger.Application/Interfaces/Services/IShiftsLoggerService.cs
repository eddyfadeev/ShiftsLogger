using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Services;

public interface IShiftsLoggerService
{
    List<Shift> GetAllShifts();
    int AddShift(Shift shift);
    Shift? GetShift(int shiftId);
    int UpdateShift(Shift shift);
    int RemoveShift(Shift shift);
}