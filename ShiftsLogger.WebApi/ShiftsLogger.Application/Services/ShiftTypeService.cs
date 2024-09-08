using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Services;

public class ShiftTypeService : IShiftTypeService
{
    private readonly IShiftTypesRepository _shiftTypesRepository;

    public ShiftTypeService(IShiftTypesRepository shiftTypesRepository)
    {
        _shiftTypesRepository = shiftTypesRepository;
    }

    public List<ShiftType> GetAllShiftTypes() => 
        _shiftTypesRepository.GetAllShiftTypes();

    public int AddShiftType(ShiftType shiftType) =>
        _shiftTypesRepository.Add(shiftType);

    public ShiftType? GetShiftType(int shiftTypeId) =>
        _shiftTypesRepository.Get(shiftTypeId);

    public int UpdateShiftType(ShiftType shiftType) =>
        _shiftTypesRepository.Update(shiftType);

    public int RemoveShiftType(ShiftType shiftType) => 
        _shiftTypesRepository.Remove(shiftType);
}