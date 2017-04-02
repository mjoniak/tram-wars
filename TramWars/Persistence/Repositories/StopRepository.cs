using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TramWars.Domain;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Persistence.Repositories
{
    public class StopRepository : IStopRepository
    {
        private readonly Lazy<IEnumerable<Stop>> _lazyStops;

        public StopRepository(IFile stopsFile)
        {
            _lazyStops = new Lazy<IEnumerable<Stop>>(() => ParseStops(stopsFile));
        }

        public IEnumerable<Stop> GetAll()
        {   
            return _lazyStops.Value;
        }

        public Stop GetClosestStopNamed(string stopName, float lat, float lon)
        {
            return GetAll().Where(x => x.Name == stopName).ToList()
                .OrderBy(x => Math.Abs(x.Latitude - lat) + Math.Abs(x.Longitude - lon))
                .First();
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
                var lineNumber = fields[4];
                var stop = new Stop(name, lat, lon);
                if (!stopsDictionary.ContainsKey(name)) 
                {
                    stopsDictionary.Add(name, new List<Stop>());
                }

                var list = stopsDictionary[name];
                var duplicate = list.FirstOrDefault(p => !AreDistinct(p, stop));
                if (duplicate == null)
                {
                    stop.AddLine(lineNumber);
                    list.Add(stop);
                }
                else 
                {
                    duplicate.AddLine(lineNumber);
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