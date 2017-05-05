using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TramWars.Domain
{
    public class StopsFile
    {
        private readonly Lazy<IEnumerable<Stop>> _lazyStops;

        public StopsFile(IEnumerable<string> lines)
        {
            _lazyStops = new Lazy<IEnumerable<Stop>>(() => ParseStops(lines));
        }

        public IEnumerable<Stop> GetAll() => _lazyStops.Value;

        private IEnumerable<Stop> ParseStops(IEnumerable<string> lines)
        {
            var stopsDictionary = new Dictionary<string, List<Stop>>();
            foreach (var line in lines)
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
                var service = new Service(lineNumber);
                if (duplicate == null)
                {
                    stop.AddService(service);
                    list.Add(stop);
                }
                else
                {
                    duplicate.AddService(service);
                }
            }

            return stopsDictionary.Values.SelectMany(p => p).ToList();
        }

        private static bool AreDistinct(Stop stop1, Stop stop2)
        {
            return Math.Abs(stop1.Latitude - stop2.Latitude) > float.Epsilon
                   || Math.Abs(stop1.Longitude - stop2.Longitude) > float.Epsilon;
        }
    }
}