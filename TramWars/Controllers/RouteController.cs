using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Dto;
using TramWars.Persistence;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("routes")]
    public class RouteController : Controller
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IUsersFacade _users;
        private readonly IStopRepository _stopRepository;
        private readonly Func<IUnitOfWork> _uowFactory;

        public RouteController(
            IRouteRepository routeRepository,
            IUsersFacade users,
            IStopRepository stopRepository, 
            Func<IUnitOfWork> uowFactory)
        {
            _users = users;
            _routeRepository = routeRepository;
            _stopRepository = stopRepository;
            _uowFactory = uowFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StopDto[] stops)
        {
            var user = await _users.GetUserAsync(User);
            var startStopDto = stops[0];
            var targetStopDto = stops[1];
            var startStop = _stopRepository.GetClosestStopNamed(startStopDto.Name, startStopDto.Lat, startStopDto.Lon);
            var targetStop = _stopRepository.GetClosestStopNamed(targetStopDto.Name, targetStopDto.Lat, targetStopDto.Lon);
            var route = new Route(user, targetStop, startStop);
            _uowFactory.Do(() => 
            {
                _routeRepository.AddRoute(route);
            });
            var dto = new RouteDto { Id = route.Id };
            return Created($"routes/{route.Id}", dto);
        }
    }
}