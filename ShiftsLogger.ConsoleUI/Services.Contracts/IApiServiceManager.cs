using Services.Contracts.Clients;

namespace Services.Contracts;

public interface IApiServiceManager
{
    IUserService User { get; }

    IShiftTypeService ShiftType { get; }

    ILocationService Location { get; }

    IShiftService Shift { get; }
}