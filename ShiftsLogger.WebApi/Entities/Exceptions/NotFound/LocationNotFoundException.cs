namespace Entities.Exceptions.NotFound;

public class LocationNotFoundException(Guid locationId) 
    : NotFoundException($"Location with ID: {locationId} doesn't exist in the database");