namespace ShiftsLogger.Domain.Interfaces;

public interface IReportModel
{
    int Id { get; init; }
    string EntityName { get; }
}