using System;

namespace TramWars.Domain
{
    public class Coords
    {
        private const float Radius = 6371000.0f;
        private const float CloseDistanceMeters = 80.0f;

        public float Lat { get; }
        public float Lng { get; }

        public Coords (float lat, float lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public double DistanceTo(Coords that)
        {
            /*
             * Haversine formula
             */

             double phi1 = Radians(this.Lat);
             double phi2 = Radians(that.Lat);
             double lambda1 = Radians(this.Lng);
             double lambda2 = Radians(that.Lng);

             double sin1 = Math.Sin((phi2 - phi1) / 2);
             double sin2 = Math.Sin((lambda2 - lambda1) / 2);

             double sqrt = Math.Sqrt(sin1 * sin1 + Math.Cos(phi1) * Math.Cos(phi2) * sin2 * sin2);
             return 2 * Radius * Math.Asin(sqrt);
        }

        public bool AreCloseTo(Coords targetCoords)
        {
            return DistanceTo(targetCoords) <= CloseDistanceMeters;
        }

        private double Radians(float angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}