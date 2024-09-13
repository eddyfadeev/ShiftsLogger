namespace ShiftsLogger.Application.Interfaces;

public interface IHttpManager
{
    Task<string?> GetAsync(Uri url);
}