using System.Security.Claims;
using System.Threading.Tasks;
using TramWars.Identity;

namespace TramWars.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        ApplicationUser Add(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user);
    }
}