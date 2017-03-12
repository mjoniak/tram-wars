using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.DTO;
using TramWars.Persistence;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("routes")]
    public class RouteController : Controller
    {
        private readonly IRouteRepository routeRepository;
        private readonly IUserRepository userRepository;
        private readonly IStopRepository stopRepository;
        private readonly Func<IUnitOfWork> uowFactory;

        public RouteController(
            IRouteRepository routeRepository, 
            IUserRepository userRepository,
            IStopRepository stopRepository, 
            Func<IUnitOfWork> uowFactory)
        {
            this.userRepository = userRepository;
            this.routeRepository = routeRepository;
            this.stopRepository = stopRepository;
            this.uowFactory = uowFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StopDTO[] stops)
        {
            var user = await userRepository.GetUserAsync(User);
            var startStopDTO = stops[0];
            var targetStopDTO = stops[1];
            var startStop = stopRepository.GetClosestStopNamed(startStopDTO.Name, startStopDTO.Lat, startStopDTO.Lon);
            var targetStop = stopRepository.GetClosestStopNamed(targetStopDTO.Name, targetStopDTO.Lat, targetStopDTO.Lon);
            var route = new Route(user, targetStop, startStop);
            uowFactory.Do(() => 
            {
                routeRepository.AddRoute(route);
            });
            var dto = new RouteDTO { Id = route.Id };
            return Created($"routes/{route.Id}", route);
        }
    }
}