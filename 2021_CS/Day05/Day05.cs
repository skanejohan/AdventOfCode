using CSharpLib;
using CSharpLib.DataStructures;
using CSharpLib.Utils;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public class Day05
    {
        public static int Part1()
        {
            var data = RealData().Select(ParseLine)
                .Where(r => LineUtils.IsHorizontal(r.X1, r.Y1, r.X2, r.Y2) || LineUtils.IsVertical(r.X1, r.Y1, r.X2, r.Y2));
            return NoOfOverlaps(data);
        }

        public static int Part2()
        {
            return NoOfOverlaps(RealData().Select(ParseLine));
        }

        private static int NoOfOverlaps(IEnumerable<(int X1, int Y1, int X2, int Y2)> lines)
        {
            var countedSet = new CountedSet<(int, int)>();
            foreach (var (X1, Y1, X2, Y2) in lines)
            {
                foreach (var (X, Y) in LineUtils.AllPoints(X1, Y1, X2, Y2))
                {
                    countedSet.Add((X, Y));
                }
            }
            return countedSet.Where(kv => kv.Item2 > 1).Count();
        }

        private static (int X1, int Y1, int X2, int Y2) ParseLine(string s)
        {
            var parts = s.Split(" -> ");
            var part1 = parts[0].Split(',');
            var part2 = parts[1].Split(',');
            return (int.Parse(part1[0]), int.Parse(part1[1]), int.Parse(part2[0]), int.Parse(part2[1]));
        }

        private static IEnumerable<string> RealData() => new DataLoader(2021, 5).ReadStrings("DataReal.txt");
        private static IEnumerable<string> TestData() => new DataLoader(2021, 5).ReadStrings("DataTest.txt");
    }
}
