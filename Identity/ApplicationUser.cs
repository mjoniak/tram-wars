using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TramWars.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName) : base(userName)
        {
        }

        // for EF
        private ApplicationUser() {}
    }
}