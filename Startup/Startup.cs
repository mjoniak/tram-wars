using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TramWars.Persistence;
using TramWars.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Startup
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
            loggerFactory.AddConsole();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //TODO: figure a way to store production connection string in a secure way
            var connection = @"Host=localhost;Username=postgres;Password=postgres;Database=TramWars;";
            services.AddDbContext<TramWarsContext>(o => o.UseNpgsql(connection));
            
            services.AddTransient<IRouteRepository, RouteRepository>();
        }
    }
}