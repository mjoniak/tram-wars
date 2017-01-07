using Microsoft.EntityFrameworkCore;
using Moq;
using TramWars.Domain;
using TramWars.Persistence;
using TramWars.Persistence.Repositories;
using Xunit;

namespace TramWars.Tests.Persistence.Repositories
{
    public class RouteRepositoryTests
    {
        Mock<DbSet<Route>> mockSet;
        Mock<TramWarsContext> mockContext;
        RouteRepository repository;

        public RouteRepositoryTests()
        {
            mockSet = new Mock<DbSet<Route>>();
            mockContext = new Mock<TramWarsContext>(new DbContextOptionsBuilder().Options);
            mockContext.Setup(p => p.Routes).Returns(mockSet.Object);
            repository = new RouteRepository(mockContext.Object);
        }

        [Fact]
        public void AddRouteAddsToContext() 
        {
            repository.AddRoute(new Route(null));
            mockSet.Verify(p => p.Add(It.IsAny<Route>()), Times.Once);
        }

        [Fact]
        public void GetRouteFindsInContext()
        {
            var routeInDb = new Route(null);
            mockSet.Setup(p => p.Find(123)).Returns(routeInDb);
            var returnedRoute = repository.Get(123);
            Assert.Same(returnedRoute, routeInDb);
        }
    }
}