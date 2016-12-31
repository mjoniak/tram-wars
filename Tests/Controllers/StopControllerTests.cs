using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.DTO;
using TramWars.Persistence.Repositories.Interfaces;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class StopControllerTests
    {
        [Fact]
        public void GetAllStopsReturnsListOfStops() 
        {
            var stopRepositoryMock = new Mock<IStopRepository>();
            stopRepositoryMock.Setup(p => p.GetAll()).Returns(new[] { new Stop("Test Stop", 50.0f, 20.0f) });
            var controller = new StopsController(stopRepositoryMock.Object);
            
            var result = controller.Get() as OkObjectResult;
            var stops = result.Value as IEnumerable<StopDTO>;
            
            Assert.NotNull(stops);
            Assert.Collection(stops, p => {
                Assert.Equal("Test Stop", p.Name);
                Assert.Equal(50.0f, p.Lat);
                Assert.Equal(20.0f, p.Lon);
            });
        }
    }
}