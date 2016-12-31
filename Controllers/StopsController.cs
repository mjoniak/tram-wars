using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.DTO;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("stops")]
    public class StopsController : Controller
    {
        private readonly IStopRepository repository;

        public StopsController(IStopRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Get()
        {
            var dtos = repository.GetAll().Select(p => new StopDTO 
            {
                Name = p.Name,
                Lat = p.Latitude,
                Lon = p.Longitude
            }).ToList();
            return Ok(dtos);
        }
    }
}