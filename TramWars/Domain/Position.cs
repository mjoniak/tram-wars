using System;

namespace TramWars.Domain
{
    public class Position
    {
        private const double Tolerance = 1e-5;

        public Position(float lat, float lng)
        {
            Lat = lat;
            Lng = lng;
        }

        // ReSharper disable once UnusedMember.Local
        private Position() {}

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int Id { get; private set; }

        public float Lat { get; private set; }

        public float Lng { get; private set; }

        public static bool operator ==(Position left, Position right) 
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return Math.Abs(left.Lat - right.Lat) < Tolerance && Math.Abs(left.Lng - right.Lng) < Tolerance;
        }

        public static bool operator !=(Position left, Position right) 
        {
            return !(left == right);
        }

        public override bool Equals (object that)
        {
            return this == (that as Position);
        }
        
        public override int GetHashCode()
        {
            return 0;
        }
    }
}