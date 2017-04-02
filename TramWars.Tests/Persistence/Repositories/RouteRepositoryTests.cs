using Microsoft.EntityFrameworkCore;
using Moq;
using TramWars.Domain;
using TramWars.Persistence;
using TramWars.Persistence.Repositories;
using TramWars.Tests.Helpers;
using Xunit;

namespace TramWars.Tests.Persistence.Repositories
{
    public class RouteRepositoryTests
    {
        private readonly Mock<DbSet<Route>> _dbSet;
        private readonly RouteRepository _repository;

        public RouteRepositoryTests()
        {
            _dbSet = new Mock<DbSet<Route>>();
            var context = new Mock<TramWarsContext>(new DbContextOptionsBuilder().Options);
            context.Setup(p => p.Routes).Returns(_dbSet.Object);
            _repository = new RouteRepository(context.Object);
        }

        [Fact]
        public void AddRouteAddsToContext() 
        {
            _repository.AddRoute(RouteFactory.CreateTestRoute());
            _dbSet.Verify(p => p.Add(It.IsAny<Route>()), Times.Once);
        }
    }
}