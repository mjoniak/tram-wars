using System.Linq;
using TramWars.Domain;
using Xunit;

namespace TramWars.Tests.Domain
{
    public class StopsFileTest
    {
        [Fact]
        public void WhenNoLinesThenReturnsEmpty()
        {
            var stopsFile = new StopsFile(Enumerable.Empty<string>());
            Assert.Empty(stopsFile.GetAll());
        }

        [Fact]
        public void WhenTwoSameStopsThenReturnOne()
        {
            var stopsFile = new StopsFile(new[]
            {
                "Test Stop,50.0,20.0,1,1",
                "Test Stop,50.0,20.0,1,2"
            });
            var stops = stopsFile.GetAll();
            Assert.Collection(stops, p =>
            {
                Assert.Equal("Test Stop", p.Name);
                Assert.Equal(50.0f, p.Latitude);
                Assert.Equal(20.0f, p.Longitude);
                Assert.Equal(2, p.Lines.Count());
            });
        }
    }
}