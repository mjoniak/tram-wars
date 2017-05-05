using TramWars.Domain;
using Xunit;

namespace TramWars.Tests.Domain
{
    public class ObjectiveTest
    {
        [Fact]
        public void WhenTwoSameStopsThenZeroPoints() 
        {
            var stop = new Stop("Test stop #1", 0.0f, 0.0f);
            var objective = new Objective(stop, stop);
            int points = objective.CalculatePoints();
            Assert.Equal(0, points);
        }

        [Fact]
        public void WhenDifferentStopsThenMoreThanZero() 
        {
            var startStop = new Stop("Test stop #1", 0.0f, 0.0f);
            var endStop = new Stop("Test stop #1", 0.0f, 10.0f);
            var objective = new Objective(startStop, endStop);
            int points = objective.CalculatePoints();
            Assert.True(points > 0);
        }
    }
}