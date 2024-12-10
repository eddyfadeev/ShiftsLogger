namespace Entities.Exceptions.NotFound;

public class ShiftTypeNotFoundException(Guid shiftTypeId) 
    : NotFoundException($"Shift type with ID: {shiftTypeId} doesn't exist in the database");