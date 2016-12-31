using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;
using TramWars.Tests.Persistence.Repositories;

namespace TramWars.Persistence.Repositories
{
    public class StopRepository : IStopRepository
    {
        private readonly Lazy<IEnumerable<Stop>> lazyStops;

        public StopRepository(IFile stopsFile)
        {
            this.lazyStops = new Lazy<IEnumerable<Stop>>(() => ParseStops(stopsFile));
        }

        public IEnumerable<Stop> GetAll()
        {   
            return lazyStops.Value;
        }

        private static IEnumerable<Stop> ParseStops(IFile stopsFile)
        {
            var stopsDictionary = new Dictionary<string, List<Stop>>();
            foreach (var line in stopsFile.GetLines()) 
            {
                var fields = line.Split(',');
                var name = fields[0];
                var lat = float.Parse(fields[1], CultureInfo.InvariantCulture);
                var lon = float.Parse(fields[2], CultureInfo.InvariantCulture);
                var stop = new Stop(name, lat, lon);
                if (!stopsDictionary.ContainsKey(name)) 
                {
                    stopsDictionary.Add(name, new List<Stop>());
                }

                var list = stopsDictionary[name];
                if (list.All(p => AreDistinct(p, stop)))
                {
                    list.Add(stop);
                }
            }
            
            return stopsDictionary.Values.SelectMany(p => p);
        }

        private static bool AreDistinct(Stop stop1, Stop stop2)
        {
            return Math.Abs(stop1.Latitude - stop2.Latitude) > float.Epsilon
                || Math.Abs(stop1.Longitude - stop2.Longitude) > float.Epsilon;
        }
    }
}