using System.Collections.Generic;
using System.Linq;

namespace TramWars.Domain
{
    public class Stop
    {
        private readonly ICollection<string> lines = new List<string>();

        public string Name { get; }
        public float Latitude { get; }
        public float Longitude { get; }

        public IEnumerable<string> GetLines()
        {
            return lines;
        }

        public double DistanceTo(Stop that)
        {
            var thisCoords = new Coords(Latitude, Longitude);
            var thatCoords = new Coords(that.Latitude, that.Longitude);
            return thisCoords.DistanceTo(thatCoords);
        }

        public Stop(string name, float latitude, float longitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public bool HasCommonLinesWith(Stop startStop)
        {
            return lines.Intersect(startStop.lines).Any();
        }

        public void AddLine(string lineNumber)
        {
            lines.Add(lineNumber);
        }
    }
}