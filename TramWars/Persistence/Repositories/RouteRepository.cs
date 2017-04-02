using System.Linq;
using Microsoft.EntityFrameworkCore;
using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Persistence.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly TramWarsContext _context;

        public RouteRepository(TramWarsContext context)
        {
            _context = context;
        }

        public Route AddRoute(Route route)
        {
            _context.Routes.Add(route);
            return route;
        }

        public Route Get(int id)
        {
            return _context.Routes
                .Include(x => x.Positions)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}