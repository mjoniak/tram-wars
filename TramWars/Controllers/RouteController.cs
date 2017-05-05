using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Dto;
using TramWars.Persistence;
using TramWars.Queries;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("routes")]
    public class RouteController : Controller
    {
        private readonly IUsersFacade _users;
        private readonly AppDbContext _dbContext;
        private readonly FindStopQuery _findStopQuery;

        public RouteController(
            FindStopQuery findStopQuery,
            IUsersFacade users,
            AppDbContext dbContext)
        {
            _findStopQuery = findStopQuery;
            _users = users;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StopDto[] stops)
        {
            var user = await _users.GetUserAsync(User);
            var startStopDto = stops[0];
            var targetStopDto = stops[1];
            // TODO: it can't work like this - stops need actual ids
            // and it has to be two specific stops, not just similarly named
            var startStop = _findStopQuery.Find(startStopDto.Name, startStopDto.Lat, startStopDto.Lon);
            var targetStop = _findStopQuery.Find(targetStopDto.Name, targetStopDto.Lat, targetStopDto.Lon);
            var route = new Route(user, targetStop, startStop);
            _dbContext.Routes.Add(route);
            _dbContext.SaveChanges();
            var dto = new RouteDto { Id = route.Id };
            return Created($"routes/{route.Id}", dto);
        }
    }
}