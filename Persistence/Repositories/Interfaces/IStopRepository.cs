using System.Collections.Generic;
using TramWars.Domain;

namespace TramWars.Persistence.Repositories.Interfaces
{
    public interface IStopRepository
    {
        IEnumerable<Stop> GetAll();
    }
}