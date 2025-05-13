using Models;

namespace Repository.Interfaces;

public interface IMemoryCacheProvider
{
    T? Get<T>(string cacheKey);
    void Save(string cacheKey, object cacheData);
}