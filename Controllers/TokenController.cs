using System;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using TramWars.Identity;

namespace TramWars.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public TokenController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OpenIdConnectRequest request)
        {
            var result = await GetUserAsync(request);
            if (!result.Exists())
            {
                // TODO: test this case
                return BadRequest();
            }

            if (!await userManager.CheckPasswordAsync(result.User, request.Password))
            {
                // TODO: test this case
                return BadRequest();
            }

            var ticket = await CreateTicketAsync(request, result.User, result.Info?.Properties);
            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        private async Task<AuthenticationTicket> CreateTicketAsync(
            OpenIdConnectRequest request, 
            ApplicationUser user,
            AuthenticationProperties properties)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            foreach (var claim in principal.Claims) 
            {
                claim.SetDestinations(
                    OpenIdConnectConstants.Destinations.AccessToken,
                    OpenIdConnectConstants.Destinations.IdentityToken);
            }

            var ticket = new AuthenticationTicket(principal, properties, OpenIdConnectServerDefaults.AuthenticationScheme);
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

        private async Task<GetUserResult> GetUserAsync(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = await userManager.FindByNameAsync(request.Username);
                return new GetUserResult(user);
            }
            else if (request.IsRefreshTokenGrantType())
            {
                var info = await HttpContext.Authentication.GetAuthenticateInfoAsync(OpenIdConnectServerDefaults.AuthenticationScheme);
                var user = await userManager.GetUserAsync(info.Principal);
                return new GetUserResult(user, info);
            }

            return new GetUserResult(null);
        }

        private class GetUserResult
        {
            public ApplicationUser User { get; }
            public AuthenticateInfo Info { get; }

            public bool Exists()
            {
                return User != null;
            }

            public GetUserResult(ApplicationUser user, AuthenticateInfo info = null)
            {
                User = user;
                Info = info;
            }
        }
    }
}