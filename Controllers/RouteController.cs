using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
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
        private readonly Func<IUnitOfWork> uowFactory;

        public RouteController(IRouteRepository routeRepository, IUserRepository userRepository, Func<IUnitOfWork> uowFactory)
        {
            this.userRepository = userRepository;
            this.routeRepository = routeRepository;
            this.uowFactory = uowFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var user = await userRepository.GetUserAsync(User);
            var route = new Route(user);
            uowFactory.Do(() => 
            {
                routeRepository.AddRoute(route);
            });
            return Created($"routes/{route.Id}", route);
        }
    }
}