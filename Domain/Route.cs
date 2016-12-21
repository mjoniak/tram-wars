using System.Collections.Generic;

namespace TramWars.Domain
{
    public class Route
    {
        public int Id { get; private set; }

        private List<Position> positions = new List<Position>();        
        
        public IEnumerable<Position> Positions
        {
            get { return positions; }
        }

        public void AddPosition(Position position)
        {
            positions.Add(position);
        }
    }
}