using TramWars.Domain;

namespace TramWars.Tests.Helpers
{
    public static class StopFactory
    {
        public static Stop CreateTestStop()
        {
            return new Stop("Test Stop", 0.0f, 0.0f);
        }
    }
}