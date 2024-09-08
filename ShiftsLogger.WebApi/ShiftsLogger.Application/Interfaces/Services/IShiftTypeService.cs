using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Services;

public interface IShiftTypeService
{
    List<ShiftType> GetAllShiftTypes();
    int AddShiftType(ShiftType shiftType);
    ShiftType? GetShiftType(int shiftTypeId);
    int UpdateShiftType(ShiftType shiftType);
    int RemoveShiftType(ShiftType shiftType);
}