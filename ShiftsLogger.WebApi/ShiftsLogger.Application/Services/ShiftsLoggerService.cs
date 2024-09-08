using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Services;

public class ShiftsLoggerService : IShiftsLoggerService
{
    private readonly IShiftsRepository _shiftsRepository;

    public ShiftsLoggerService(IShiftsRepository shiftsRepository)
    {
        _shiftsRepository = shiftsRepository;
    }

    public List<Shift> GetAllShifts() =>
        _shiftsRepository.GetAllShifts();
    
    public int AddShift(Shift shift) =>
        _shiftsRepository.Add(shift);

    public Shift? GetShift(int shiftId) =>
        _shiftsRepository.Get(shiftId);
    
    public int UpdateShift(Shift shift) =>
        _shiftsRepository.Update(shift);
    
    public int RemoveShift(Shift shift) =>
        _shiftsRepository.Remove(shift);
}