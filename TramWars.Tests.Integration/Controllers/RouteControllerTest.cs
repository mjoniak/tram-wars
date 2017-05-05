using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Dto;
using TramWars.Persistence;
using TramWars.Queries;
using TramWars.Tests.Helpers;
using TramWars.Tests.Integration.Helpers;
using Xunit;

namespace TramWars.Tests.Integration.Controllers
{
    public class RouteControllerTest
    {
        private readonly RouteController _controller;
        private readonly StopDto[] _stopDtos;
        private readonly AppDbContext _context;

        public RouteControllerTest()
        {
            _context = AppDbContextFactory.TestDbContext();
            var testUser = new AppUser("TestUser");
            _context.Users.Add(testUser);
            var start = Factories.TestStop("TestStart");
            var end = Factories.TestStop("TestEnd");
            _context.Stops.Add(start);
            _context.Stops.Add(end);
            _context.SaveChanges();

            _stopDtos = new[] { new StopDto { Name = start.Name }, new StopDto { Name = end.Name} };
            var facade = new Mock<IUsersFacade>();
            facade.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _controller = new RouteController(new FindStopQuery(_context), facade.Object, _context);
        }

        [Fact]
        public async Task PostRouteCreatesRouteInDb()
        {
            await _controller.Post(_stopDtos);
            Assert.Equal(1, _context.Routes.Count());
        }

        [Fact]
        public async Task PostRouteReturnsRoute()
        {
            var result = await _controller.Post(_stopDtos) as CreatedResult;
            Assert.NotNull(result);
            Assert.IsType<RouteDto>(result.Value);
            Assert.Equal("routes/1", result.Location);
        }
    }
}