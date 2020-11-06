using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    internal static class Day06
    {
        public static int Part1() => Orbiters.Keys.Aggregate(0, (total, next) => total + NoOfOrbits(next, 0));

        public static int Part2()
        {
            var myPath = PathToCom("YOU");
            var santasPath = PathToCom("SAN");

            var firstCommonOrbiter = myPath.First(santasPath.Contains);
            var myCountUntilCommonOrbiter = myPath.IndexOf(firstCommonOrbiter) - 1;
            var santasCountUntilCommonOrbiter = santasPath.IndexOf(firstCommonOrbiter) - 1;
            return myCountUntilCommonOrbiter + santasCountUntilCommonOrbiter;
        }

        static Day06()
        {
            Orbiters = new Dictionary<string, string>();
            foreach (var line in DataReader.ReadStrings("Day06Input.txt"))
            {
                var center = line.Split(")")[0];
                var orbiter = line.Split(")")[1];
                Orbiters[orbiter] = center;
            }
        }

        private static Dictionary<string, string> Orbiters; // Dictionary from orbiter to center

        private static int NoOfOrbits(string orbiter, int count) => orbiter == "COM" ? count : NoOfOrbits(Orbiters[orbiter], count + 1);

        private static List<string> PathToCom(string orbiter)
        {
            var result = new List<string>();
            recFn(orbiter, result);
            return result;

            void recFn(string orbiter, List<string> result)
            {
                result.Add(orbiter);
                if (orbiter == "COM")
                {
                    return;
                }
                recFn(Orbiters[orbiter], result);
            }
        }
    }
}