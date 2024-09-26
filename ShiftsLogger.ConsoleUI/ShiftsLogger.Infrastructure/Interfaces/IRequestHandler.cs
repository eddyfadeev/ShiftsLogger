namespace ShiftsLogger.Infrastructure.Interfaces;

public interface IRequestHandler
{
    internal Task<string> GetAsync(Uri url);
    internal Task<HttpResponseMessage> PostAsync(Uri url, HttpContent content);
    internal Task<HttpResponseMessage?> DeleteAsync(Uri url);
}