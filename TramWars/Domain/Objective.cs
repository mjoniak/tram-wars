namespace TramWars.Domain
{
    public class Objective
    {
        private const int PointsPerKm = 100;

        public Objective(Stop startStop, Stop endStop)
        {
            this.StartStop = startStop;
            this.EndStop = endStop;
        }

        public Stop StartStop { get; }
        public Stop EndStop { get; }

        public int CalculatePoints()
        {
            var distanceKm = StartStop.DistanceTo(EndStop) / 1000;
            return (int)(distanceKm * PointsPerKm);
        }
    }
}