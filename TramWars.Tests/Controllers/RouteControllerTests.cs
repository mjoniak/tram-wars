using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Dto;
using TramWars.Persistence;
using TramWars.Persistence.Repositories.Interfaces;
using TramWars.Tests.Helpers;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class RouteControllerTests
    {
        private readonly RouteController _controller;
        private readonly Mock<IRouteRepository> _routeRepository;
        private readonly StopDto[] _stopDtos;

        public RouteControllerTests()
        {
            _stopDtos = new[] { new StopDto(), new StopDto() };
            _routeRepository = new Mock<IRouteRepository>();
            var users = new Mock<IUsersFacade>();
            var stopRepository = new Mock<IStopRepository>();
            stopRepository.Setup(x => x.GetClosestStopNamed(It.IsAny<string>(), It.IsAny<float>(), It.IsAny<float>()))
                .Returns(StopFactory.CreateTestStop());
            _controller = new RouteController(_routeRepository.Object, users.Object, stopRepository.Object, () => Mock.Of<IUnitOfWork>());
        }

        [Fact]
        public async Task PostRouteCreatesRouteInDb()
        {
            await _controller.Post(_stopDtos);
            _routeRepository.Verify(p => p.AddRoute(It.IsAny<Route>()), Times.Once);
        }

        [Fact]
        public async Task PostRouteReturnsRoute()
        {
            var result = await _controller.Post(_stopDtos) as CreatedResult;
            Assert.NotNull(result);
            Assert.IsType<RouteDto>(result.Value);
            Assert.Equal("routes/0", result.Location);
        }
    }
}