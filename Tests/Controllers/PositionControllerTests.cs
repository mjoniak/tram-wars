using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class PositionControllerTests
    {
        PositionController controller;
        Route routeInDb;
        Mock<IRouteRepository> repositoryMock;

        public PositionControllerTests()
        {
            repositoryMock = new Mock<IRouteRepository>();
            controller = new PositionController(repositoryMock.Object);
            routeInDb = new Route();            
            repositoryMock.Setup(p => p.Get(1)).Returns(routeInDb);   
        }

        [Fact]
        public void PostPositionReturnsCreated()
        {   
            var result = controller.Post(1, new Position(50.0f, 20.0f)) as CreatedResult;
            var returnedPos = result.Value as Position;
            Assert.Equal(50.0f, returnedPos.Lat);
            Assert.Equal(20.0f, returnedPos.Lng);
            Assert.Equal("routes/1/positions/0", result.Location);
        }

        [Fact]
        public void PostPositionSavesToRepository()
        {
            var position =  new Position(50.0f, 20.0f);
            controller.Post(1, position);
            Assert.Contains(position, routeInDb.Positions);
            repositoryMock.Verify(p => p.SaveChanges());
        }
    }
}