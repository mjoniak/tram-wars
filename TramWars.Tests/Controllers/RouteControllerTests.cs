using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.DTO;
using TramWars.Persistence;
using TramWars.Persistence.Repositories.Interfaces;
using TramWars.Tests.Helpers;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class RouteControllerTests
    {
        RouteController controller;
        Mock<IRouteRepository> repositoryMock;
        Mock<IUserRepository> userRepositoryMock;
        StopDTO[] stopDTOs;

        public RouteControllerTests()
        {
            stopDTOs = new[] { new StopDTO(), new StopDTO() };
            repositoryMock = new Mock<IRouteRepository>();
            userRepositoryMock = new Mock<IUserRepository>();
            var stopRepostitoryMock = new Mock<IStopRepository>();
            stopRepostitoryMock.Setup(x => x.GetClosestStopNamed(It.IsAny<string>(), It.IsAny<float>(), It.IsAny<float>()))
                .Returns(StopFactory.CreateTestStop());
            controller = new RouteController(repositoryMock.Object, userRepositoryMock.Object, stopRepostitoryMock.Object, () => Mock.Of<IUnitOfWork>());
        }

        [Fact]
        public void PostRouteCreatesRouteInDb()
        {
            controller.Post(stopDTOs).Wait();
            repositoryMock.Verify(p => p.AddRoute(It.IsAny<Route>()), Times.Once);
        }

        [Fact]
        public void PostRouteReturnsRoute()
        {
            var result = controller.Post(stopDTOs).Result as CreatedResult;
            Assert.IsType<Route>(result.Value);
            Assert.Equal("routes/0", result.Location);
        }
    }
}