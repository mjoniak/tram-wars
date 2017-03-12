namespace TramWars.Domain
{
    public class Position
    {
        public Position(float lat, float lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }

        private Position() {}

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

            return left.Lat == right.Lat && left.Lng == right.Lng;
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