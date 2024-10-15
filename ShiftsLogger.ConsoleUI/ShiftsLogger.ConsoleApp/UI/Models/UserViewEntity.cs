using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class UserViewEntity : ViewModelEntity
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string? LastName { get; init; }
    public string Email { get; init; }
    public string? Role { get; init; }

    public override string GetShortRepresentation()
    {
        var info = $"{Id}. {FirstName}{LastName}";
        
        SetElementHeight(1);

        return info;
    }

    public override string GetDetailedRepresentation()
    {
        var info = 
            $"""
            {Id}. {FirstName} {LastName}
            Email: {Email}
            Role: {Role ?? string.Empty}
            """;
        
        SetElementHeight(3);

        return info;
    }
}