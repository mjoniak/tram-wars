using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Dto;
using TramWars.Persistence;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("stops")]
    public class StopsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public StopsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Get()
        {
            var dtos = _dbContext.Stops.Select(p => new StopDto
            {
                Name = p.Name,
                Lat = p.Latitude,
                Lon = p.Longitude
            }).ToList();
            return Ok(dtos);
        }
    }
}