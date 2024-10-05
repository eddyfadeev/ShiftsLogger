using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class UserViewEntity : ViewModelEntity
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string? LastName { get; init; }
    public string Email { get; init; }
    public string? Role { get; init; }
    
    public UserViewEntity()
    {
        SetElementHeight(3);
    }
}