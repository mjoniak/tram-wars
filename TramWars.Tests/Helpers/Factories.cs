using TramWars.Domain;

namespace TramWars.Tests.Helpers
{
    public static class Factories
    {
        public static Route TestRoute()
        {
            return new Route(new AppUser("Test User"), TestStop(), TestStop());
        }

        public static Stop TestStop(string name = "Test Stop")
        {
            return new Stop(name, 0.0f, 0.0f);
        }
    }
}