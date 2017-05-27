using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TramWars.Domain;
using TramWars.Persistence;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("routes/{routeId}/positions")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _dbContext;

        public PositionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int routeId, [FromBody] Position position)
        {
            // TODO: check if authorized to access route
            var route = await _dbContext.Routes
                .Include(x => x.Positions)
                .Include(x => x.StartStop)
                .Include(x => x.TargetStop)
                .Include(x => x.User)
                .SingleAsync(x => x.Id == routeId);
            route.AddPosition(position);
            if (route.IsFinished())
            {
                var user = route.User;
                var objective = new Objective(route.StartStop, route.TargetStop);
                user.AddScore(objective.CalculatePoints());
                route.Close();
            }

            await _dbContext.SaveChangesAsync();
            return Created($"routes/{routeId}/positions/{position.Id}", position);
        }
    }
}