using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TramWars.Persistence;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TramWars.Domain;
using TramWars.Queries;

namespace TramWars.Startup
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Config.Init(env.IsDevelopment());
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseIdentity();
            app.UseOAuthValidation();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = Config.JwtAuthority,
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
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseNpgsql(Config.ConnectionString);
                o.UseOpenIddict();
            });

            services.AddIdentity<AppUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<AppDbContext, int>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddOpenIddict(o =>
            {
                o.AddEntityFrameworkCoreStores<AppDbContext>();
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

            services.AddTransient<IUsersFacade, UsersFacade>();
            services.AddTransient<FindStopQuery>();
        }
    }
}