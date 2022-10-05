using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day12
    {
        public static long Part1()
        {
            return FindNoOfPaths(GetMap("DataReal.txt"), CanVisitSmallCave);

            static bool CanVisitSmallCave(CountedSet<string> visited, string cave)
            {
                return visited.Occurs(cave) == 0;
            }
        }

        public static long Part2()
        {
            return FindNoOfPaths(GetMap("DataReal.txt"), CanVisitSmallCave);

            static bool CanVisitSmallCave(CountedSet<string> visited, string cave)
            {
                var visitedTwice = visited.All().Where(x => x.Item == x.Item.ToLower() && x.Count == 2);
                return visitedTwice.Count() == 0 || (visitedTwice.Count() == 1 && visited.Occurs(cave) == 0);
            }
        }

        private static int FindNoOfPaths(Dictionary<string, List<string>> map, Func<CountedSet<string>, string, bool> canVisitSmallCave)
        {
            var result = 0;
            Visit("start", new CountedSet<string>());
            return result;

            void Visit(string cave, CountedSet<string> visited)
            {
                if (cave == "end")
                {
                    result++;
                    return;
                }
                if (cave != "start" && cave.ToLower() == cave && !canVisitSmallCave(visited, cave))
                {
                    return;
                }
                visited.Add(cave);
                foreach (var neighbor in map[cave])
                {
                    Visit(neighbor, visited);
                }
                visited.Remove(cave);
            }
        }

        private static Dictionary<string, List<string>> GetMap(string fileName)
        {
            var map = new Dictionary<string, List<string>>();
            foreach (var line in new DataLoader(2021, 12).ReadStrings(fileName))
            {
                var from = line.Split('-')[0];
                var to = line.Split('-')[1];
                if (from == "start" || to == "end")
                {
                    map.AddToList(from, to);
                }
                else if (from == "end" || to == "start")
                {
                    map.AddToList(to, from);
                }
                else
                {
                    map.AddToList(from, to);
                    map.AddToList(to, from);
                }
            }
            return map;
        }
    }
}
