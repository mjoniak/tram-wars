using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class RouteControllerTests
    {
        RouteController controller;
        Mock<IRouteRepository> repository;

        public RouteControllerTests()
        {
            repository = new Mock<IRouteRepository>();
            controller = new RouteController(repository.Object);
        }

        [Fact]
        public void PostRouteCreatesRouteInDb()
        {
            repository.Setup(p => p.AddRoute(It.IsAny<Route>())).Verifiable();
            controller.Post();
            repository.Verify(p => p.AddRoute(It.IsAny<Route>()), Times.Once);
        }

        [Fact]
        public void PostRouteReturnsRoute()
        {
            var result = controller.Post() as CreatedResult;
            Assert.IsType<Route>(result.Value);
            Assert.Equal("routes/0", result.Location);
        }
    }
}