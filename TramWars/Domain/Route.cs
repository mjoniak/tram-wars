using System.Collections.Generic;
using System.Linq;

namespace TramWars.Domain
{
    public sealed class Route
    {
        public Route(AppUser user, Stop targetStop, Stop startStop) : this()
        {
            User = user;
            TargetStop = targetStop;
            StartStop = startStop;
        }

        private Route() 
        {
            Positions = new List<Position>();
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int Id { get; private set; }

        public AppUser User { get; private set; }
        
        public bool IsClosed { get; private set; }

        public Stop StartStop { get; private set; }

        public Stop TargetStop { get; private set; }

        public ICollection<Position> Positions { get; private set; }

        public bool IsFinished()
        {
            if (IsClosed || !Positions.Any()) 
            {
                return false;
            }

            var currentPos = Positions.Last();
            var coords = new Coords(currentPos.Lat, currentPos.Lng);
            var targetCoords = TargetStop.GetCoords();
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

        public void Close() 
        {
            IsClosed = true;
        }
    }
}