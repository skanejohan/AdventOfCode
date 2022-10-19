using System;

namespace _2021_CS.Day23
{
    public static class Solver
    {
        public static long Part1()
        {
            BurrowTransformerTests.Run();
            var burrowTransformer = new BurrowTransformer(false);
            var dijkstra = new CSharpLib.Algorithms.Dijkstra<string>();
            return dijkstra.Solve(
                ".......BC..BA..DA..DC..", 
                ".......AA..BB..CC..DD..", 
                burrowTransformer.Neighbors);
        }

        public static long Part2()
        {
            var burrowTransformer = new BurrowTransformer(true);
            var dijkstra = new CSharpLib.Algorithms.Dijkstra<string>();
            return dijkstra.Solve(
                ".......BDDCBCBADBAADACC",
                ".......AAAABBBBCCCCDDDD",
                burrowTransformer.Neighbors);
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

        private static void DumpPath(CSharpLib.Algorithms.Dijkstra<string> dijkstra, bool large)
        {
            foreach (var (T, Distance) in dijkstra.ShortestPath())
            {
                Console.WriteLine($"{DumpBurrow(T, large)} @ {Distance}");
                Console.WriteLine();
            }
        }
    }
}

