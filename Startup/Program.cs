using Microsoft.AspNetCore.Hosting;

namespace TramWars.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: load ip from config
            var host = new WebHostBuilder()
                .UseUrls("http://localhost:5000")
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
