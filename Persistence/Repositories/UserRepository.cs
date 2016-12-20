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
            return user;
        }
    }
}