using Priority_Queue;
using System;
using System.Collections.Generic;

namespace _2021_CS.Day23
{
    public static class Solver
    {
        public static long Part1()
        {
            return Solve(".......BC..BA..DA..DC..", ".......AA..BB..CC..DD..", false);
        }

        public static long Part2()
        {
            return Solve(".......BDDCBCBADBAADACC", ".......AAAABBBBCCCCDDDD", true);
        }

        private static string DumpBurrow(string burrow, bool large)
        {
            return (large
                ? "#############\n#ab.c.d.e.fg#\n###h#i#j#k###\n  #l#m#n#o#  \n  #p#q#r#s#  \n  #t#u#v#w#  \n  #########  "
                : "#############\n#ab.c.d.e.fg#\n###h#i#j#k###\n  #l#m#n#o#  \n  #########  ")
                .Replace('a', burrow[0]).Replace('b', burrow[1]).Replace('c', burrow[2]).Replace('d', burrow[3])
                .Replace('e', burrow[4]).Replace('f', burrow[5]).Replace('g', burrow[6]).Replace('h', burrow[7])
                .Replace('i', burrow[11]).Replace('j', burrow[15]).Replace('k', burrow[19]).Replace('l', burrow[8])
                .Replace('m', burrow[12]).Replace('n', burrow[16]).Replace('o', burrow[20]).Replace('p', burrow[9])
                .Replace('q', burrow[13]).Replace('r', burrow[17]).Replace('s', burrow[21]).Replace('t', burrow[10])
                .Replace('u', burrow[14]).Replace('v', burrow[18]).Replace('w', burrow[22]);
        }

        private static void DumpPath(Dictionary<string, (string, int)> edges, string target)
        {
            var cell = target;
            var strings = new List<string>();
            strings.Add(cell);
            while (edges[cell].Item1 != "")
            {
                strings.Add($"{DumpBurrow(edges[cell].Item1, false)} @ {edges[cell].Item2}");
                cell = edges[cell].Item1;
            }
            strings.Reverse();
            foreach (var s in strings)
            {
                Console.WriteLine(s);
                Console.WriteLine();
            }
        }

        private static int Solve(string burrow, string target, bool largeRooms, Dictionary<string, (string, int)>? edges = null)
        {
            var visited = new HashSet<string>();
            var queue = new SimplePriorityQueue<string, int>();
            var dist = new Dictionary<string, int>() { [burrow] = 0 };
            var burrowTransformer = new BurrowTransformer(largeRooms);

            if (edges != null)
            {
                edges[burrow] = ("", 0);
            }

            queue.Enqueue(burrow, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!visited.Contains(current))
                {
                    visited.Add(current);
                    if (current == target)
                    {
                        break;
                    }

                    var currentCost = dist[current];

                    foreach (var (nextCost, next) in burrowTransformer.Neighbors(current))
                    {
                        var alt = currentCost + nextCost;
                        if (alt < dist.GetValueOrDefault(next, int.MaxValue))
                        {
                            dist[next] = alt;
                            if (edges != null)
                            {
                                edges[next] = (current, alt);
                            }
                            queue.Enqueue(next, alt);
                        }
                    }
                }
            }

            return dist[target];
        }
    }
}
