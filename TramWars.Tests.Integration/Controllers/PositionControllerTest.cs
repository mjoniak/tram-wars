using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Tests.Helpers;
using TramWars.Tests.Integration.Helpers;
using Xunit;

namespace TramWars.Tests.Integration.Controllers
{
    public class PositionControllerTest
    {
        private readonly PositionController _controller;
        private readonly Route _routeInDb;

        public PositionControllerTest()
        {
            var context = AppDbContextFactory.TestDbContext();
            _routeInDb = Factories.TestRoute();
            context.Routes.Add(_routeInDb);
            context.SaveChanges();
            _controller = new PositionController(context);
        }

        [Fact]
        public async Task PostPosition()
        {
            var position = new Position(50.0f, 20.0f);
            var result = (CreatedResult) await _controller.Post(_routeInDb.Id, position);
            var returnedPos = (Position) result.Value;

            Assert.Equal(50.0f, returnedPos.Lat);
            Assert.Equal(20.0f, returnedPos.Lng);
            Assert.Equal($"routes/{_routeInDb.Id}/positions/{position.Id}", result.Location);
            Assert.Contains(position, _routeInDb.Positions);
        }
    }
}