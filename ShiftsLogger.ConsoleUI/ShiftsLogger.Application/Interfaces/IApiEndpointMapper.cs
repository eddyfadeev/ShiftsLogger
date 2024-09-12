namespace ShiftsLogger.Application.Interfaces;

public interface IApiEndpointMapper
{
    Uri GetRelativeUrl<TApi>(TApi endpoint) where TApi : Enum;
}