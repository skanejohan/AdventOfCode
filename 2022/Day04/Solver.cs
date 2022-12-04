using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day04
{
    public static class Solver
    {
        public static long Part1()
        {
            return LoadData("data.txt").Count(s => s.Item1.IsSubsetOf(s.Item2) || s.Item2.IsSubsetOf(s.Item1));
        }

        public static long Part2()
        {
            return LoadData("data.txt").Count(s => s.Item1.Intersect(s.Item2).Any());
        }

        private static IEnumerable<(HashSet<int>, HashSet<int>)> LoadData(string fileName)
        {
            return new DataLoader(2022, 4).ReadStrings(fileName).Select(ParseLine);

            (HashSet<int>, HashSet<int>) ParseLine(string line)
            {
                var split = line.Split(',');
                var first = split[0].Split('-');
                var second = split[1].Split('-');
                var a = int.Parse(first[0]);
                var b = int.Parse(first[1]);
                var c = int.Parse(second[0]);
                var d = int.Parse(second[1]);
                return (
                    new HashSet<int>(Enumerable.Range(a, b - a + 1)),
                    new HashSet<int>(Enumerable.Range(c, d - c + 1)));
            }
        }
    }
}
