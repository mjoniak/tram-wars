namespace TramWars.Domain
{
    public class Stop
    {
        public string Name { get; }
        public float Latitude { get; }
        public float Longitude { get; }

        public Stop(string name, float latitude, float longitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}