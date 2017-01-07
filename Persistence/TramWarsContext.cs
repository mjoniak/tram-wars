using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TramWars.Domain;
using TramWars.Identity;

namespace TramWars.Persistence
{
    public class TramWarsContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public TramWarsContext(DbContextOptions options) : base(options) {}

        public virtual DbSet<Route> Routes { get; set; }

        void IUnitOfWork.Commit()
        {
            SaveChanges();
        }

        void IUnitOfWork.Rollback()
        {
            var entries = ChangeTracker.Entries();
            entries.Where(x => x.State == EntityState.Modified).ToList().ForEach(x => 
            {
                x.CurrentValues.SetValues(x.OriginalValues);
                x.State = EntityState.Unchanged;
            });
            entries.Where(x => x.State == EntityState.Deleted).ToList().ForEach(x => x.State = EntityState.Unchanged);
            entries.Where(x => x.State == EntityState.Added).ToList().ForEach(x => x.State = EntityState.Detached);
        }
    }
}
