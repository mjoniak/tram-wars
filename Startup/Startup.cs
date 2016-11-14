using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TramWars.Persistence;

namespace TramWars.Startup
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //TODO: figure a way to store production connection string in a secure way
            var connection = @"Host=localhost;Username=postgres;Database=TramWars;";
            services.AddDbContext<TramWarsContext>(o => o.UseNpgsql(connection));
        }
    }
}