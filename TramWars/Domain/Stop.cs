using System.Collections.Generic;
using System.Linq;

namespace TramWars.Domain
{
    public class Stop
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }
        public ICollection<Service> Lines { get; private set; }

        public double DistanceTo(Stop that)
        {
            var thisCoords = new Coords(Latitude, Longitude);
            var thatCoords = new Coords(that.Latitude, that.Longitude);
            return thisCoords.DistanceTo(thatCoords);
        }

        private Stop()
        {
            Lines = new List<Service>();
        }

        public Stop(string name, float latitude, float longitude) : this()
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public bool HasCommonLinesWith(Stop startStop)
        {
            return Lines
                .Select(x => x.Name)
                .Intersect(startStop.Lines.Select(y => y.Name))
                .Any();
        }

        public void AddService(Service service)
        {
            Lines.Add(service);
        }

        public Coords GetCoords()
        {
            return new Coords(Latitude, Longitude);
        }
    }
}