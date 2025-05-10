using Models;

namespace Repository.Interfaces;

public interface ISessionRepository
{
    SessionData? Get();
    void Save(SessionData session);
}