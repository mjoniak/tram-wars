using System;
using Microsoft.AspNetCore.Mvc;

namespace TramWars.Controllers
{
    [Route("/api/route")]
    public class RouteController : Controller
    {
        public IActionResult Post(string routeName)
        {
            return null;
        }
    }
}