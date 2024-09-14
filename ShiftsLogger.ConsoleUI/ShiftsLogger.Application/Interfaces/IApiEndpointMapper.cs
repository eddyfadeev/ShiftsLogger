namespace ShiftsLogger.Application.Interfaces;

public interface IApiEndpointMapper
{
    Uri GetRelativeUrl<TApi>(TApi endpoint, int? id = null) where TApi : Enum;
}