using Microsoft.EntityFrameworkCore;
using TramWars.Persistence;
using TramWars.Startup;

namespace TramWars.Tests.Integration.Helpers
{
    static class AppDbContextFactory
    {
        static AppDbContextFactory() => Config.Init(isDevelopment: true);

        public static AppDbContext TestDbContext()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(Config.TestConnectionString);
            var context = new AppDbContext(builder.Options);

            // recreate database for each integration test
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            return context;
        }
    }
}
