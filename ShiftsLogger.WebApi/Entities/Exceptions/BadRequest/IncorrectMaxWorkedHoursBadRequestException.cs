namespace Entities.Exceptions.BadRequest;

public class IncorrectMaxWorkedHoursBadRequestException() 
    : BadRequestException("Max Worked Hours cannot be less than Min Worked Hours");