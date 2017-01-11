using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TramWars.Identity;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;

        }

        public ApplicationUser Add(ApplicationUser user, string password)
        {
            var result = userManager.CreateAsync(user, password).Result;
            if (!result.Succeeded) 
            {
                throw new InvalidOperationException(string.Join(Environment.NewLine, result.Errors.Select(p => p.Description)));
            }

            return user;
        }

        public IEnumerable<ApplicationUser> GetByTopScores(int n)
        {
            return userManager.Users
                .Where(x => x.Score > 0)
                .OrderBy(x => x.Score)
                .Take(n).ToList();
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user)
        {
            return await userManager.GetUserAsync(user) as ApplicationUser;
        }
    }
}