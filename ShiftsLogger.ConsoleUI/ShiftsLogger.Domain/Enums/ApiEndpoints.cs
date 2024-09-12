namespace ShiftsLogger.Domain.Enums;

public static class ApiEndpoints
{
    public enum Shifts
    {
        CreateNew,
        GetAll,
        ActionById
    }

    public enum Users
    {
        CreateNew,
        GetAll,
        ActionById,
        GetShiftsByUserId
    }
    
    public enum Locations
    {
        CreateNew,
        GetAll,
        ActionById,
        GetShiftsByLocationId
    }
    
    public enum ShiftTypes
    {
        CreateNew,
        GetAll,
        ActionById,
        GetShiftsByTypeId
    }
}