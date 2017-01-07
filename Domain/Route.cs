using System.Collections.Generic;
using System.Linq;
using TramWars.Identity;

namespace TramWars.Domain
{
    public class Route
    {
        public Route(ApplicationUser user) 
        {
            User = user;
        }

        private Route() {}

        public int Id { get; private set; }

        public ApplicationUser User { get; private set; }

        private ICollection<Position> positions = new List<Position>();        
        
        public IEnumerable<Position> Positions
        {
            get { return positions; }
        }

        public void AddPosition(Position position)
        {
            var lastPosition = positions.OrderBy(x => x.Id).LastOrDefault();
            if (position != lastPosition) 
            {
                positions.Add(position);
            }
        }
    }
}