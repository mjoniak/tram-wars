using System;
using System.Linq;
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
    }
}