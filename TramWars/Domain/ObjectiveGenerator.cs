using System;
using System.Collections.Generic;
using System.Linq;

namespace TramWars.Domain
{
    public class ObjectiveGenerator
    {
        private const int MaxObjectivesPerStop = 10;
        private const float SwitchProbability = 0.33f;
        private readonly IEnumerable<Stop> _stops;

        public ObjectiveGenerator(IEnumerable<Stop> stops)
        {
            _stops = stops;
        }

        public IEnumerable<Objective> Generate(Stop startStop)
        {
            var foundStops = new HashSet<Stop>();
            var visitedStops = new HashSet<Stop>();
            while (foundStops.Count < MaxObjectivesPerStop)
            {
                var stop = FindTargetStop(startStop, visitedStops);
                if (stop == null)
                {
                    break;
                }

                foundStops.Add(stop);
            }

            return foundStops.Select(x => new Objective(startStop, x));
        }

        private Stop FindTargetStop(Stop startStop, HashSet<Stop> visitedStops)
        {
            int seed = (DateTime.Today - DateTime.MinValue).Seconds;
            var rand = new Random(seed);

            var stop = _stops
                .Where(x => x != startStop && x.HasCommonLinesWith(startStop) && !visitedStops.Contains(x))
                .OrderBy(x => rand.Next())
                .FirstOrDefault();
            if (stop == null)
            {
                return null;
            }

            visitedStops.Add(stop);

            if (rand.Next((int)(100 / SwitchProbability * 100.0f)) == 0)
            {
                stop = FindTargetStop(stop, visitedStops) ?? stop;
            }

            return stop;
        }
    }
}