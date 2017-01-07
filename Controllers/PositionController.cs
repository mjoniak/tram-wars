using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Persistence;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("routes/{routeId}/positions")]
    public class PositionController : Controller
    {
        private readonly IRouteRepository repository;
        private readonly Func<IUnitOfWork> uowFactory;

        public PositionController(IRouteRepository repository, Func<IUnitOfWork> uowFactory)
        {
            this.uowFactory = uowFactory;
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Post(int routeId, [FromBody] Position position)
        {
            Route route = repository.Get(routeId);
            uowFactory.Do(() => 
            {
                route.AddPosition(position);
            });
            return Created($"routes/{routeId}/positions/{position.Id}", position);
        }
    }
}