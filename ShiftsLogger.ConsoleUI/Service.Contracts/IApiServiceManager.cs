using Service.Contracts.Clients;

namespace Service.Contracts;

public interface IApiServiceManager
{
    IUserService User { get; }

    IShiftTypeService ShiftType { get; }

    ILocationService Location { get; }

    IShiftService Shift { get; }
}