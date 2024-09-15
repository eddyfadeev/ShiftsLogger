namespace ShiftsLogger.Application.Interfaces;

public interface IHttpManager
{
    HttpClient GetHttpClient();
    void EnsureSuccessStatusCode(HttpResponseMessage response, Uri url);
}