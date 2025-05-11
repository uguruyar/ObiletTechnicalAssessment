using Models;

namespace Services.Interfaces;

public interface IObiletApiClient
{
    Task<SessionData> GetSessionAsync();
    Task<string> CallObiletEndpoint(string url, object body);
}