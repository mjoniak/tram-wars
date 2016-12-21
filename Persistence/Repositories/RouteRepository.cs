using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Persistence.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private TramWarsContext context;

        public RouteRepository(TramWarsContext context)
        {
            this.context = context;
        }

        public Route AddRoute(Route route)
        {
            context.Routes.Add(route);
            context.SaveChanges();
            return route;
        }

        public Route Get(int id)
        {
            return context.Routes.Find(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}