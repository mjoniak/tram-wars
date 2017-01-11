using System.Linq;
using Moq;
using TramWars.Persistence.Repositories;
using Xunit;

namespace TramWars.Tests.Persistence.Repositories
{
    public class StopRepositoryTests
    {
        [Fact]
        public void WhenNoLinesThenReturnsEmpty() {
            var fileMock = new Mock<IFile>();
            var repository = new StopRepository(fileMock.Object);
            var stops = repository.GetAll();
            Assert.Empty(stops);
        }

        [Fact]
        public void WhenTwoSameStopsThenReturnOne() 
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(p => p.GetLines()).Returns(new[] 
            { 
                "Test Stop,50.0,20.0,1,1", 
                "Test Stop,50.0,20.0,1,2" 
            });
            var repository = new StopRepository(fileMock.Object);
            var stops = repository.GetAll();
            Assert.Collection(stops, p => 
            {
                Assert.Equal("Test Stop", p.Name);
                Assert.Equal(50.0f, p.Latitude);
                Assert.Equal(20.0f, p.Longitude);
                Assert.Equal(2, p.GetLines().Count());
            });
        }
    }
}