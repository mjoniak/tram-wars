using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Core;
using TramWars.Identity;
using TramWars.Services.Interfaces;

namespace TramWars.Services
{
    public class UserService : IUserService
    {
        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        
        public async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, ApplicationUser user)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            foreach (var claim in principal.Claims) 
            {
                claim.SetDestinations(
                    OpenIdConnectConstants.Destinations.AccessToken,
                    OpenIdConnectConstants.Destinations.IdentityToken);
            }

            var ticket = new AuthenticationTicket(
                principal, 
                new AuthenticationProperties(), 
                OpenIdConnectServerDefaults.AuthenticationScheme);
            if (!request.IsRefreshTokenGrantType()) 
            {
                ticket.SetScopes(new[] 
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.Roles                
                }.Intersect(request.GetScopes()));
            }

            return ticket;
        }
        
        public async Task<ApplicationUser> GetUserAsync(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = await userManager.FindByNameAsync(request.Username);
                return user;
            }

            return null;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
    }
}