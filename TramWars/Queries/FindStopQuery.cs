using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TramWars.Domain;
using TramWars.Persistence;

namespace TramWars.Queries
{
    public class FindStopQuery
    {
        private readonly AppDbContext _context;

        public FindStopQuery(AppDbContext context)
        {
            _context = context;
        }

        public Stop Find(string stopName, float lat, float lon)
        {
            return _context.Stops
                .Include(x => x.Lines)
                .Where(x => x.Name == stopName).ToList()
                .OrderBy(x => Math.Abs(x.Latitude - lat) + Math.Abs(x.Longitude - lon))
                .First();
        }
    }
}
