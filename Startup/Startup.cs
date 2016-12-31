using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TramWars.Persistence;
using TramWars.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using TramWars.Persistence.Repositories.Interfaces;
using TramWars.Identity;
using TramWars.Services.Interfaces;
using TramWars.Services;
using TramWars.Tests.Persistence.Repositories;

namespace TramWars.Startup
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseOAuthValidation();
            app.UseOpenIddict();
            app.UseMvc();
            loggerFactory.AddConsole();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //TODO: figure a way to store production connection string in a secure way
            var connection = @"Host=localhost;Username=postgres;Password=postgres;Database=TramWars;";
            services.AddDbContext<TramWarsContext>(o => {
                o.UseNpgsql(connection);
                o.UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TramWarsContext>()
                .AddDefaultTokenProviders();

            services.AddOpenIddict()
                .AddEntityFrameworkCoreStores<TramWarsContext>()
                .AddMvcBinders()
                .EnableTokenEndpoint("/token")
                .AllowPasswordFlow()
                .DisableHttpsRequirement() // TODO: only for debug!
                .AddEphemeralSigningKey(); // TODO: change for production

            services.AddCors(o =>
                o.AddPolicy("CorsPolicy", builder => 
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            
            services.AddTransient<IRouteRepository, RouteRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStopRepository, StopRepository>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IFile, StopsFile>();
        }
    }
}