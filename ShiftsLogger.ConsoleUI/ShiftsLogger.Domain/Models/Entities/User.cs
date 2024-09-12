using Shared.Interfaces;

namespace Shared.Models.Entities;

public class User : IReportModel
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string? LastName { get; init; }
    public string Email { get; init; }
    public string? Role { get; init; }
}