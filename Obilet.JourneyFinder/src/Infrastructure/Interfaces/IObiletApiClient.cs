using Models;

namespace Infrastructure.Interfaces;

public interface IObiletApiClient
{
    Task<SessionData> GetSessionAsync();
    Task<string> CallObiletEndpoint(string url, object body);
}