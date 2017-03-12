using Microsoft.AspNetCore.Hosting;

namespace TramWars.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseUrls("http://192.168.2.154:5000", "http://localhost:5000")
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
