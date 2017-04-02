using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TramWars.Domain
{
    public interface IUsersFacade
    {
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<AppUser> GetUserAsync(ClaimsPrincipal principal);
        Task<IEnumerable<AppUser>> GetByTopScoresAsync(int count);
    }

    public class UsersesFacade : IUsersFacade
    {
        private readonly UserManager<AppUser> _manager;

        public UsersesFacade(UserManager<AppUser> manager) => _manager = manager;

        public Task<IdentityResult> CreateAsync(AppUser user, string password) => _manager.CreateAsync(user, password);
        public Task<AppUser> GetUserAsync(ClaimsPrincipal principal) => _manager.GetUserAsync(principal);

        public async Task<IEnumerable<AppUser>> GetByTopScoresAsync(int count) => 
            await _manager.Users.OrderByDescending(x => x.Score).Take(count).ToListAsync();
    }
}
