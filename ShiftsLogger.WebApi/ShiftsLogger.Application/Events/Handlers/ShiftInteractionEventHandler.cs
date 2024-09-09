using System.ComponentModel.DataAnnotations;
using ShiftsLogger.Application.Interfaces.Data.Repository;
using ShiftsLogger.Application.Interfaces.Events;
using ShiftsLogger.Domain.Events;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Events.Handlers;

public class ShiftInteractionEventHandler : IEventHandler<DatabaseInteractionEvent<Shift>> 
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IGenericRepository<ShiftType> _shiftTypeRepository;
    
    public ShiftInteractionEventHandler(
        IGenericRepository<User> userRepository,
        IGenericRepository<Location> locationRepository,
        IGenericRepository<ShiftType> shiftTypeRepository)
    {
        _userRepository = userRepository;
        _locationRepository = locationRepository;
        _shiftTypeRepository = shiftTypeRepository;
    }

    public async Task HandleAsync(DatabaseInteractionEvent<Shift> @event)
    {
        var shift = @event.Entity;

        if (!await _userRepository.ExistsAsync(shift.UserId))
        {
            throw new ValidationException("Invalid user ID. User does not exist.");
        }
        
        if (!await _locationRepository.ExistsAsync(shift.LocationId))
        {
            throw new ValidationException("Invalid location ID. Location does not exist.");
        }
        
        if (!await _shiftTypeRepository.ExistsAsync(shift.ShiftTypeId))
        {
            throw new ValidationException("Invalid shift type ID. Shift type does not exist.");
        }
    }
}