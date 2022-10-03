using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day08
    {
        public static long Part1()
        {
            var allOutputValues = RealData().Select(d => d.OutputValues).SelectMany(o => o);
            var noOf1 = allOutputValues.Where(s => s.Length == 2).Count();
            var noOf4 = allOutputValues.Where(s => s.Length == 4).Count();
            var noOf7 = allOutputValues.Where(s => s.Length == 3).Count();
            var noOf8 = allOutputValues.Where(s => s.Length == 7).Count();
            return noOf1 + noOf4 + noOf7 + noOf8;
        }

        public static long Part2()
        {
            long total = 0;
            foreach (var (SignalPatterns, OutputValues) in RealData())
            {
                var d = new HashSet<char>[10];
                var sp = SignalPatterns.Select(x => x.ToHashSet()).ToArray();
                var ov = OutputValues.ToList();

                // These can be calculated by number of segments
                d[1] = sp.Single(p => p.Count() == 2);
                d[4] = sp.Single(p => p.Count() == 4);
                d[7] = sp.Single(p => p.Count() == 3);
                d[8] = sp.Single(p => p.Count() == 7);

                // These can be calculated by number of segments, and number of segments in common with 1 and 4
                d[0] = sp.Single(p => p.Count() == 6 && p.Intersect(d[1]).Count() == 2 && p.Intersect(d[4]).Count() == 3);
                d[2] = sp.Single(p => p.Count() == 5 && p.Intersect(d[1]).Count() == 1 && p.Intersect(d[4]).Count() == 2);
                d[3] = sp.Single(p => p.Count() == 5 && p.Intersect(d[1]).Count() == 2 && p.Intersect(d[4]).Count() == 3);
                d[5] = sp.Single(p => p.Count() == 5 && p.Intersect(d[1]).Count() == 1 && p.Intersect(d[4]).Count() == 3);
                d[6] = sp.Single(p => p.Count() == 6 && p.Intersect(d[1]).Count() == 1 && p.Intersect(d[4]).Count() == 3);
                d[9] = sp.Single(p => p.Count() == 6 && p.Intersect(d[1]).Count() == 2 && p.Intersect(d[4]).Count() == 4);

                total += Decode(ov[0], d) * 1000 + Decode(ov[1], d) * 100 + Decode(ov[2], d) * 10 + Decode(ov[3], d);
            }

            int Decode(string outputValue, HashSet<char>[] digits)
            {
                return Enumerable.Range(0, 10).Single(i => digits[i].SetEquals(outputValue));
            }

            return total;
        }

        private static IEnumerable<(IEnumerable<string> SignalPatterns, IEnumerable<string> OutputValues)> RealData()
        {
            var lines = new DataLoader(2021, 8).ReadStrings("DataReal.txt");
            foreach (var line in lines)
            {
                var sp = line.Split(" | ")[0];
                var op = line.Split(" | ")[1];
                yield return (sp.Split(' '), op.Split(' '));
            }
        }
    }
}
