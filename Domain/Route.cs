using System.Collections.Generic;
using System.Linq;

namespace TramWars.Domain
{
    public class Route
    {
        public IEnumerable<Position> Positions { get; } = Enumerable.Empty<Position>();
    }
}