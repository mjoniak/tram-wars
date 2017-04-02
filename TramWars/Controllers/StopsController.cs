using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TramWars.Dto;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [Route("stops")]
    public class StopsController : Controller
    {
        private readonly IStopRepository _repository;

        public StopsController(IStopRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Get()
        {
            var dtos = _repository.GetAll().Select(p => new StopDto
            {
                Name = p.Name,
                Lat = p.Latitude,
                Lon = p.Longitude
            }).ToList();
            return Ok(dtos);
        }
    }
}