using System;

namespace TramWars.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}