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
        Task<AppUser> FindAsync(string username);
        Task ChangePasswordAsync(AppUser user, string oldPassword, string password);
        Task UpdateAsync(AppUser user);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
    }

    public class UsersFacade : IUsersFacade
    {
        private readonly UserManager<AppUser> _manager;

        public UsersFacade(UserManager<AppUser> manager) => _manager = manager;

        public Task<IdentityResult> CreateAsync(AppUser user, string password) => _manager.CreateAsync(user, password);
        public Task<AppUser> GetUserAsync(ClaimsPrincipal principal) => _manager.GetUserAsync(principal);

        public async Task<IEnumerable<AppUser>> GetByTopScoresAsync(int count) => 
            await _manager.Users.OrderByDescending(x => x.Score).Take(count).ToListAsync();

        public async Task<AppUser> FindAsync(string username) => await _manager.FindByNameAsync(username);
        public async Task ChangePasswordAsync(AppUser user, string oldPassword, string password)
        {
            await _manager.ChangePasswordAsync(user, oldPassword, password);
        }

        public Task UpdateAsync(AppUser user) => _manager.UpdateAsync(user);
        public async Task<bool> CheckPasswordAsync(AppUser user, string password) => await _manager.CheckPasswordAsync(user, password);
    }
}
