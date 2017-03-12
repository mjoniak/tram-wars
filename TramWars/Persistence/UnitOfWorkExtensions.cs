using System;

namespace TramWars.Persistence
{
    public static class UnitOfWorkExtensions
    {
        public static void Do(this Func<IUnitOfWork> uowFactory, Action work) 
        {
            using (var uow = uowFactory()) 
            {
                try 
                {
                    work();
                    uow.Commit();
                } 
                catch (Exception)
                {
                    uow.Rollback();
                    throw;
                }
            }
        }
    }
}