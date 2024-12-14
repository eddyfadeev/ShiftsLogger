namespace Shared.Dto.Location;

public abstract record LocationForManipulationDto
{
    private string _address;
    private string _name;

    public string Name
    {
        get => _name;
        init => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }

    public string Address
    {
        get => _address;
        init => _address = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
}