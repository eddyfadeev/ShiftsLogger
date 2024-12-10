namespace Entities.Exceptions.NotFound;

public class ShiftNotFoundException(Guid shiftId) 
    : NotFoundException($"Shift with ID: {shiftId} doesn't exist in the database");