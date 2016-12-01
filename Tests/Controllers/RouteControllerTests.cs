using Microsoft.AspNetCore.Mvc;
using TramWars.Controllers;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class RouteControllerTests
    {
        [Fact]
        public void PostRouteReturnsId()
        {
            RouteController controller = new RouteController();
            var result = controller.Post("routeName");
            Assert.IsType<OkObjectResult>(result);
        } 
    }
}