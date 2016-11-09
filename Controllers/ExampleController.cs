using Microsoft.AspNetCore.Mvc;

namespace TramWars.Controllers
{
    [Route("api/example")]
    public class ExampleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("test");
        }
    } 
}