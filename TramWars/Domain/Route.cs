using System.Collections.Generic;
using System.Linq;
using TramWars.Identity;

namespace TramWars.Domain
{
    public class Route
    {
        public Route(ApplicationUser user, Stop targetStop, Stop startStop) : this()
        {
            User = user;
            SetTargetStop(targetStop);
            SetStartStop(startStop);
        }

        private Route() 
        {
            Positions = new List<Position>();
        }

        public int Id { get; private set; }

        public ApplicationUser User { get; private set; }

        public float TargetLat { get; private set; }

        public float TargetLng { get; private set; }

        public string TargetStopName { get; private set; }
        
        public float StartLat { get; private set; }

        public float StartLng { get; private set; }

        public string StartStopName { get; private set; }

        public bool IsClosed { get; private set; }

        public virtual ICollection<Position> Positions { get; set; }

        public bool IsFinished()
        {
            if (IsClosed || !Positions.Any()) 
            {
                return false;
            }

            var currentPos = Positions.Last();
            var coords = new Coords(currentPos.Lat, currentPos.Lng);
            var targetCoords = new Coords(TargetLat, TargetLng);
            return coords.AreCloseTo(targetCoords);
        }

        public void AddPosition(Position position)
        {
            var lastPosition = Positions.OrderBy(x => x.Id).LastOrDefault();
            if (position != lastPosition) 
            {
                Positions.Add(position);
            }
        }

        public void SetTargetStop(Stop stop) 
        {
            TargetStopName = stop.Name;
            TargetLat = stop.Latitude;
            TargetLng = stop.Longitude;
        }
        
        public void SetStartStop(Stop stop) 
        {
            StartStopName = stop.Name;
            StartLat = stop.Latitude;
            StartLng = stop.Longitude;
        }

        public Stop GetTargetStop()
        {
            return new Stop(TargetStopName, TargetLat, TargetLng);
        }
        
        public Stop GetStartStop()
        {
            return new Stop(StartStopName, StartLat, StartLng);
        }

        public void Close() 
        {
            IsClosed = true;
        }
    }
}