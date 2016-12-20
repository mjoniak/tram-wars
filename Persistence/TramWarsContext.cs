using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using TramWars.Domain;
using TramWars.Identity;

namespace TramWars.Persistence
{
    public class TramWarsContext : IdentityDbContext<ApplicationUser>
    {
        public TramWarsContext(DbContextOptions options) : base(options) {}

        public virtual DbSet<Route> Routes { get; set; }
    }
}
