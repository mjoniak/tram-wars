using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Route("routes")]
    public class RouteController : Controller
    {
        private readonly IRouteRepository repository;

        public RouteController(IRouteRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Post()
        {
            var route = new Route();
            repository.AddRoute(route);
            return Created($"routes/{route.Id}", route);
        }
    }
}