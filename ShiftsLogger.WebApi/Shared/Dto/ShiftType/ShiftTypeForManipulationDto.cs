namespace Shared.Dto.ShiftType;

public abstract record ShiftTypeForManipulationDto
{
    private string _name;

    public string Name
    {
        get => _name;
        init => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
}