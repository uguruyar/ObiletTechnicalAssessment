using Microsoft.Extensions.Caching.Memory;
using Models;
using Repository.Interfaces;

namespace Repository;

public class InMemorySessionRepository(IMemoryCache memoryCache) : ISessionRepository
{
    private const string CacheKey = "ObiletSession";

    public SessionData? Get()
    {
        memoryCache.TryGetValue(CacheKey, out SessionData? session);
        return session;
    }

    public void Save(SessionData session)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            //Session expire time. Session will be expired automatically after 1 day
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) 
        };

        memoryCache.Set(CacheKey, session, cacheOptions);
    }
}