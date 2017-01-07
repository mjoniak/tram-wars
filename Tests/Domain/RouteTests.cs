using System.Linq;
using TramWars.Domain;
using Xunit;

namespace TramWars.Tests.Domain
{
    public class RouteTests
    {
        [Fact]
        public void NewRouteIsEmpty()
        {
            Route route = new Route(null);
            Assert.Empty(route.Positions);
        }

        [Fact]
        public void CantAddSamePositionTwice() 
        {
            Route route = new Route(null);
            route.AddPosition(new Position(50.0f, 20.0f));
            route.AddPosition(new Position(50.0f, 20.0f));
            Assert.Equal(1, route.Positions.Count());
        }
    }
}