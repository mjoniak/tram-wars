using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Route("routes/{routeId}/positions")]
    public class PositionController : Controller
    {
        private IRouteRepository repository;

        public PositionController(IRouteRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Post(int routeId, [FromBody] Position position)
        {
            Route route = repository.Get(routeId);
            route.AddPosition(position);
            repository.SaveChanges();
            return Created($"routes/{routeId}/positions/{position.Id}", position);
        }
    }
}