namespace ShiftsLogger.Domain.Interfaces;

public interface IReportModelWithId : IReportModel
{
    int Id { get; init; }
}