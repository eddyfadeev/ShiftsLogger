namespace Entities.Exceptions.NotFound;

public class UserNotFoundException(Guid userId) 
    : NotFoundException($"User with ID: {userId} doesn't exist in the database");