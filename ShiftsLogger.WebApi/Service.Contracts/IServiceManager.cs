namespace Service.Contracts;

public interface IServiceManager
{
    IShiftService ShiftService { get; }
    IUserService UserService { get; }
    IShiftTypeService ShiftTypeService { get; }
    ILocationService LocationService { get; }
}