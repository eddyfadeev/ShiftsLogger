using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entities;

public class User : IReportModel
{
    public int Id { get; init; }
    public string EntityName => "User";
    public string FirstName { get; init; }
    public string? LastName { get; init; }
    public string Email { get; init; }
    public string? Role { get; init; }

    public override string ToString() =>
        $"""
        {FirstName} {LastName ?? string.Empty}
        Email: {Email}
        Role: {Role ?? string.Empty}
        """;
}