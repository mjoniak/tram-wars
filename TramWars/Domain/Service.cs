namespace TramWars.Domain
{
    public class Service
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Service(string name)
        {
            Name = name;
        }

        private Service()
        {
        }
    }
}