using TramWars.Domain;

namespace TramWars.Tests.Helpers
{
    public static class RouteFactory
    {
        public static Route CreateTestRoute()
        {
            return new Route(null, StopFactory.CreateTestStop(), StopFactory.CreateTestStop());
        }
    }
}