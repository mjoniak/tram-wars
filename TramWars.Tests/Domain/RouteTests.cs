using System.Linq;
using TramWars.Domain;
using TramWars.Tests.Helpers;
using Xunit;

namespace TramWars.Tests.Domain
{
    public class RouteTests
    {
        [Fact]
        public void NewRouteIsEmpty()
        {
            Route route = RouteFactory.CreateTestRoute();
            Assert.Empty(route.Positions);
        }

        [Fact]
        public void CantAddSamePositionTwice() 
        {
            Route route = RouteFactory.CreateTestRoute();
            route.AddPosition(new Position(50.0f, 20.0f));
            route.AddPosition(new Position(50.0f, 20.0f));
            Assert.Equal(1, route.Positions.Count());
        }
    }
}