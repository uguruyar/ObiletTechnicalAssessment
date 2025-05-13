using Microsoft.Extensions.Caching.Memory;
using Models;
using Repository.Interfaces;

namespace Repository;

// Refactor here. 
public class MemoryCacheProvider(IMemoryCache memoryCache) : IMemoryCacheProvider
{
    private const string CacheKey = "ObiletSession";
    
    // This method only works if 1 instance runs.
    // Sessions for 2 or more instances can be kept in Redis.
    public SessionData? Get()
    {
        memoryCache.TryGetValue(CacheKey, out SessionData? session);
        return session;
    }
    
    // public T? Get<T>()
    // {
    //     memoryCache.TryGetValue(CacheKey, out T? cacheData);
    //     return cacheData;
    // }
    
    public void Save(object cacheData)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            //Session expire time. Session will be expired automatically after 1 day
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) 
        };
    
        memoryCache.Set(CacheKey, cacheData, cacheOptions);
    }
}