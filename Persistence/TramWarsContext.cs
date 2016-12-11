using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using TramWars.Domain;

namespace TramWars.Persistence
{
    public class TramWarsContext : DbContext
    {
        public TramWarsContext(DbContextOptions options) : base(options) {}

        public virtual DbSet<Route> Routes { get; set; }
    }
}
