using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TramWars.Persistence;
using TramWars.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using TramWars.Persistence.Repositories.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TramWars.Domain;

namespace TramWars.Startup
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseIdentity();
            app.UseOAuthValidation();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = "http://192.168.2.154:5000/", //TODO: configuration
                Audience = "resource-server", //TODO: make this a const
                RequireHttpsMetadata = false, //TODO: fix that
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = OpenIdConnectConstants.Claims.Subject,
                    RoleClaimType = OpenIdConnectConstants.Claims.Role
                }
            });

            app.UseOpenIddict();
            app.UseMvc();
            loggerFactory.AddConsole();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //TODO: figure a way to store production connection string in a secure way
            var connection = @"Host=localhost;Username=postgres;Password=postgres;Database=TramWars;";
            services.AddDbContext<TramWarsContext>(o =>
            {
                o.UseNpgsql(connection);
                o.UseOpenIddict();
            });

            services.AddIdentity<AppUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<TramWarsContext, int>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddOpenIddict(o =>
            {
                o.AddEntityFrameworkCoreStores<TramWarsContext>();
                o.AddMvcBinders();
                o.EnableTokenEndpoint("/connect/token");
                o.AllowPasswordFlow();
                o.DisableHttpsRequirement(); //TODO: fix that!
                o.UseJsonWebTokens();
                o.AddEphemeralSigningKey();
            });

            //TODO: limit CORS to the website
            services.AddCors(o =>
                o.AddPolicy("CorsPolicy", builder => 
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            
            services.AddTransient<Func<IUnitOfWork>>(x => () => x.GetService(typeof(TramWarsContext)) as IUnitOfWork);
            services.AddTransient<IRouteRepository, RouteRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStopRepository, StopRepository>();

            services.AddTransient<IFile, StopsFile>();
        }
    }
}