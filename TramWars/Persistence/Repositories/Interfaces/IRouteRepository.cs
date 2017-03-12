using TramWars.Domain;

namespace TramWars.Persistence.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        Route AddRoute(Route route);

        Route Get(int id);
    }
}