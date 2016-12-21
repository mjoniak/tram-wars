using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Mvc;
using TramWars.Services.Interfaces;

namespace TramWars.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private readonly IUserService userService;

        public TokenController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OpenIdConnectRequest request)
        {
            var user = await userService.GetUserAsync(request);
            if (user == null)
            {
                return BadRequest();
            }

            if (!await userService.CheckPasswordAsync(user, request.Password))
            {
                return BadRequest();
            }

            var ticket = await userService.CreateTicketAsync(request, user);
            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }
    }
}