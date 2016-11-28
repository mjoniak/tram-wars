using TramWars.Domain;
using Xunit;

namespace TramWars.Tests
{
    public class RouteTests
    {
        [Fact]
        public void NewRouteIsEmpty()
        {
            Route route = new Route();
            Assert.Empty(route.Positions);
        }
    }
}