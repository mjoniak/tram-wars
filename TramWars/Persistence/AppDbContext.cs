using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TramWars.Domain;

namespace TramWars.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public virtual DbSet<Route> Routes { get; set; }

        public virtual DbSet<Stop> Stops { get; set; }
    }
}
