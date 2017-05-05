using System;
using TramWars.Domain;
using Xunit;

namespace TramWars.Tests.Domain
{
    public class StopTest
    {
        [Fact]
        public void CalculatesDistance() 
        {
            var stop1 = new Stop("Krakow", 19.96f, 50.06f);
            var stop2 = new Stop("Warsaw", 21.02f, 52.26f);
            double distance = stop1.DistanceTo(stop2);
            const double expectedDistance = 255732;
            Assert.True(Math.Abs(distance - expectedDistance) <= 2000.0);
        }
    }
}