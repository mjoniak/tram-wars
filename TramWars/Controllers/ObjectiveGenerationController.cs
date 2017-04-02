using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Dto;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    public class ObjectiveGenerationController : Controller
    {
        private readonly IStopRepository _stopRepository;
        public ObjectiveGenerationController(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        [Route("stops/{stopName},{lat},{lon}/objectives")]
        public ActionResult Get(string stopName, float lat, float lon)
        {
            var stops = _stopRepository.GetAll();
            var startStop = _stopRepository.GetClosestStopNamed(stopName, lat, lon);
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