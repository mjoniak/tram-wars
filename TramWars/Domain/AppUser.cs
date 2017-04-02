using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TramWars.Domain
{
    public sealed class AppUser : IdentityUser<int>
    {
        public int Score { get; private set; }

        public AppUser(string userName)
        {
            UserName = userName;
        }

        public AppUser() { }

        public void AddScore(int value) => Score += value;
    }
}
