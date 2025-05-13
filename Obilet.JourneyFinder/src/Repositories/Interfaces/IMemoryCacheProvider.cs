using Models;

namespace Repository.Interfaces;

public interface IMemoryCacheProvider
{
    SessionData? Get();
    // T? Get<T>();
    void Save(object cacheData);
}