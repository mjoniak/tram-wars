using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace TramWars.Persistence
{
    public class TramWarsContext : DbContext
    {
        public TramWarsContext(DbContextOptions options) : base(options) {}

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
