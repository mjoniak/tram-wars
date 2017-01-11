using System.Linq;
using TramWars.Domain;
using Xunit;

namespace TramWars.Tests.Domain
{
    public class ObjectiveGeneratorTests
    {
        [Fact]
        public void WhenNoStopsThenNoObjectives() 
        {
            var startStop = new Stop("TestStop", 10.0f, 20.0f);
            var service = new ObjectiveGenerator(new[] { startStop, });
            var objectives = service.Generate(startStop);
            Assert.Empty(objectives);
        }

        [Fact]
        public void WhenTwoStopsThenOnlyObjectiveIsBetweenThem()
        {
            var startStop = new Stop("Start Stop", 10.0f, 20.0f);
            startStop.AddLine("1");
            var endStop = new Stop("End Stop", 15.0f, 20.0f);
            endStop.AddLine("1");
            var service = new ObjectiveGenerator(new[] { startStop, endStop });
            var objectives = service.Generate(startStop);
            Assert.Collection(objectives, x => Assert.Equal(endStop, x.EndStop));
        }

        [Fact]
        public void WhenMultipleStopsThenManyObjectives()
        {
            var stops = Enumerable.Range(0, 100).Select(i => 
            {
                var stop = new Stop("Stop" + i, 0.0f + i, 0.0f);
                stop.AddLine("1");
                return stop;
            });
            var service = new ObjectiveGenerator(stops);
            var objectives = service.Generate(stops.First());
            Assert.Equal(10, objectives.Count());
        }

        [Fact]
        public void WhenNoConnectionBetweenStopsThenNoResults()
        {
            var startStop = new Stop("Start Stop", 10.0f, 20.0f);
            startStop.AddLine("1");
            var endStop = new Stop("End Stop", 15.0f, 20.0f);
            endStop.AddLine("2");
            var service = new ObjectiveGenerator(new[] { startStop, endStop });
            var objectives = service.Generate(startStop);
            Assert.Empty(objectives);
        }
    }
}