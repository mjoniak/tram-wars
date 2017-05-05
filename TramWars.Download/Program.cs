using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TramWars.Persistence;
using TramWars.Startup;

namespace TramWars.Download
{
    class Program
    {
        static void Main()
        {
            var file = new StopsFile(Directory.GetFiles("Scripts/Output").SelectMany(File.ReadLines));
            var options = new DbContextOptionsBuilder().UseNpgsql(Config.ConnectionString).Options;
            Config.Init(isDevelopment: true);
            using (var context = new AppDbContext(options))
            {
                context.Stops.RemoveRange(context.Stops);
                context.Stops.AddRange(file.GetAll());
                context.SaveChanges();
            }
        }
    }
}