using System.Collections.Generic;
using TramWars.Domain;

namespace TramWars.Persistence.Repositories.Interfaces
{
    public interface IStopRepository
    {
        IEnumerable<Stop> GetAll();

        Stop GetClosestStopNamed(string stopName, float lat, float lon);
    }
}