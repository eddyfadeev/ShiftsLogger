namespace Entities.Exceptions.BadRequest;

public class MaxWorkedHoursRangeBadRequestException() 
    : BadRequestException("Max Worked Hours cannot be less than Min Worked Hours");