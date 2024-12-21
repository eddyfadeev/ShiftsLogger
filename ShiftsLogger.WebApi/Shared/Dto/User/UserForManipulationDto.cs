namespace Shared.Dto.User;

public abstract record UserForManipulationDto
{
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _role;

    public string? FirstName
    {
        get => _firstName;
        init => _firstName = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
    
    public string? LastName 
    {
        get => _lastName;
        init => _lastName = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
    
    public string? Email 
    {
        get => _email;
        init => _email = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
    
    public string? Role 
    {
        get => _role;
        init => _role = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
}