﻿namespace ShiftsLogger.Domain.Models.Dto;

public record UserDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
    public List<ShiftDto> Shifts { get; init; }
}