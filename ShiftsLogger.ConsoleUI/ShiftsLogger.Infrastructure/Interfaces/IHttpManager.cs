namespace ShiftsLogger.Infrastructure.Interfaces;

public interface IHttpManager
{
    internal HttpClient GetHttpClient();
    internal void EnsureSuccessStatusCode(HttpResponseMessage response, Uri url);
}