using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using TramWars.Identity;

namespace TramWars.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, ApplicationUser user);

        Task<ApplicationUser> GetUserAsync(OpenIdConnectRequest request);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}