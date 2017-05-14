using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TramWars.Domain;
using TramWars.Dto;
using TramWars.Persistence;
using TramWars.Queries;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    public class ObjectiveGenerationController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly FindStopQuery _findStopQuery;

        public ObjectiveGenerationController(AppDbContext dbContext, FindStopQuery findStopQuery)
        {
            _dbContext = dbContext;
            _findStopQuery = findStopQuery;
        }

        [Route("stops/{stopName},{lat},{lon}/objectives")]
        public ActionResult Get(string stopName, float lat, float lon)
        {
            var stops = _dbContext.Stops.Include(x => x.Lines).ToList();
            var startStop = _findStopQuery.Find(stopName, lat, lon);
            var generator = new ObjectiveGenerator(stops);
            var objectives = generator.Generate(startStop);
            return Ok(objectives.Select(x => new ObjectiveDto
            {
                EndStop = new StopDto
                {
                    Name = x.EndStop.Name,
                    Lat = x.EndStop.Latitude,
                    Lon = x.EndStop.Longitude
                },
                Points = x.CalculatePoints()
            }));
        }
    }
}