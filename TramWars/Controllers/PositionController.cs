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
        private readonly IRouteRepository _repository;
        private readonly Func<IUnitOfWork> _uowFactory;

        public PositionController(
            IRouteRepository repository, 
            Func<IUnitOfWork> uowFactory)
        {
            _uowFactory = uowFactory;
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post(int routeId, [FromBody] Position position)
        {
            var route = _repository.Get(routeId);
            _uowFactory.Do(() =>
            {
                route.AddPosition(position);
                if (route.IsFinished())
                {
                    var user = route.User;
                    var objective = new Objective(route.GetStartStop(), route.GetTargetStop());
                    user.AddScore(objective.CalculatePoints());
                    route.Close();
                }
            });
            return Created($"routes/{routeId}/positions/{position.Id}", position);
        }
    }
}