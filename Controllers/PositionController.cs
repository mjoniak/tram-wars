using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Persistence;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    //[Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("routes/{routeId}/positions")]
    public class PositionController : Controller
    {
        private readonly IRouteRepository repository;
        private readonly Func<IUnitOfWork> uowFactory;
        private readonly IUserRepository userRepository;

        public PositionController(
            IRouteRepository repository, 
            IUserRepository userRepository, 
            Func<IUnitOfWork> uowFactory)
        {
            this.userRepository = userRepository;
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
                if (route.IsFinished())
                {
                    var user = route.User;
                    var objective = new Objective(route.GetStartStop(), route.GetTargetStop());
                    user.AddScore(objective.CalculatePoints());
                }
            });
            return Created($"routes/{routeId}/positions/{position.Id}", position);
        }
    }
}