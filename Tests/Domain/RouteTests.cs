using TramWars.Domain;
using Xunit;

namespace TramWars.Tests.Domain
{
    public class RouteTests
    {
        [Fact]
        public void NewRouteIsEmpty()
        {
            Route route = new Route();
            Assert.Empty(route.Positions);
        }

        [Fact]
        public void CanAddPositionToRoute() 
        {
            Route route = new Route();
            route.AddPosition(new Position());
        }
    }
}