namespace ShiftsLogger.Application.Interfaces;

public interface IRequestHandler
{
    Task<string> GetAsync(Uri url);
    Task<HttpResponseMessage> PostAsync(Uri url, HttpContent content);
    Task<HttpResponseMessage?> DeleteAsync(Uri url);
}