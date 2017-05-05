using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Dto;
using TramWars.Persistence;
using TramWars.Tests.Integration.Helpers;
using Xunit;

namespace TramWars.Tests.Integration.Controllers
{
    public class StopControllerTest
    {
        private readonly AppDbContext _context;

        public StopControllerTest()
        {
            var testStop = new Stop("Test Stop", 50.0f, 20.0f);
            _context = AppDbContextFactory.TestDbContext();
            _context.Stops.RemoveRange(_context.Stops.ToList());
            _context.Stops.Add(testStop);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllStopsReturnsListOfStops()
        {
            var controller = new StopsController(_context);
            
            var result = controller.Get() as OkObjectResult;

            Assert.NotNull(result);
            var stops = result.Value as IEnumerable<StopDto>;
            Assert.NotNull(stops);
            Assert.Collection(stops, p => {
                Assert.Equal("Test Stop", p.Name);
                Assert.Equal(50.0f, p.Lat);
                Assert.Equal(20.0f, p.Lon);
            });
        }
    }
}