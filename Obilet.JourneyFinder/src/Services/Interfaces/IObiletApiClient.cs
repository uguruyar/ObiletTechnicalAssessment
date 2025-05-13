using Models;

namespace Services.Interfaces;

public interface IObiletApiClient
{
    Task<SessionData> GetSessionAsync();
    Task<T> CallObiletEndpoint<T>(string url, object body);
}