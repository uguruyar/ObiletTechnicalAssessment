using Microsoft.Extensions.Caching.Memory;
using Repository.Interfaces;

namespace Repository;

// Refactor here. 
public class MemoryCacheProvider(IMemoryCache memoryCache) : IMemoryCacheProvider
{
    // This method only works if 1 instance runs.
    // Sessions for 2 or more instances can be kept in Redis.
    public T? Get<T>(string cacheKey)
    {
        memoryCache.TryGetValue(cacheKey, out T? TData);
        return TData;
    }

    public void Save(string cacheKey, object cacheData)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            //Session expire time. Session will be expired automatically after 1 day
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        };

        memoryCache.Set(cacheKey, cacheData, cacheOptions);
    }
}