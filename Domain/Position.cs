namespace TramWars.Domain
{
    public class Position
    {
        public Position(float lat, float lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }

        public int Id { get; private set; }

        public float Lat { get; }

        public float Lng { get; }
    }
}