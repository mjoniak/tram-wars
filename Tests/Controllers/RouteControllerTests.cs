using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Persistence;
using TramWars.Persistence.Repositories.Interfaces;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class RouteControllerTests
    {
        RouteController controller;
        Mock<IRouteRepository> repositoryMock;
        Mock<IUserRepository> userRepositoryMock;

        public RouteControllerTests()
        {
            repositoryMock = new Mock<IRouteRepository>();
            userRepositoryMock = new Mock<IUserRepository>();
            controller = new RouteController(repositoryMock.Object, userRepositoryMock.Object, () => Mock.Of<IUnitOfWork>());
        }

        [Fact]
        public void PostRouteCreatesRouteInDb()
        {
            controller.Post().Wait();
            repositoryMock.Verify(p => p.AddRoute(It.IsAny<Route>()), Times.Once);
        }

        [Fact]
        public void PostRouteReturnsRoute()
        {
            var result = controller.Post().Result as CreatedResult;
            Assert.IsType<Route>(result.Value);
            Assert.Equal("routes/0", result.Location);
        }
    }
}