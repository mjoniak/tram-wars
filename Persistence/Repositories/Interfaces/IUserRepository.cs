using TramWars.Identity;

namespace TramWars.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        ApplicationUser Add(ApplicationUser user, string password);
    }
}