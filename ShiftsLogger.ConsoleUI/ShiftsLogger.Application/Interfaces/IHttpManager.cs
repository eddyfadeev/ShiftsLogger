namespace ShiftsLogger.Application.Interfaces;

public interface IHttpManager
{
    Task<string> GetAsync(Uri url);
    Task PostAsync<TEntity>(Uri url, TEntity data);
    Task<HttpResponseMessage?> DeleteAsync(Uri url, int id);
}