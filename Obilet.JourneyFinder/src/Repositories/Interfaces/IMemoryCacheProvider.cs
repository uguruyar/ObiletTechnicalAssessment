using Models;

namespace Repository.Interfaces;

public interface IMemoryCacheProvider
{
    SessionData? Get();
    void Save(SessionData session);
}