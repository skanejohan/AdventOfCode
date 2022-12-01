using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day07
    {
        public static long Part1()
        {
            return MinAlignmentCost(RealData(), (a, b) => Math.Abs(b - a));
        }

        public static long Part2()
        {
            return MinAlignmentCost(RealData(), (a, b) => (Math.Abs(b - a) * (Math.Abs(b - a) + 1)) / 2);
        }

        private static long MinAlignmentCost(IEnumerable<int> positions, Func<int, int, long> moveCost)
        {
            return Enumerable.Range(positions.Min(), positions.Max() - positions.Min() + 1).Select(p => AlignmentCost(positions, p, moveCost)).Min();
        }

        private static long AlignmentCost(IEnumerable<int> positions, int moveTo, Func<int, int, long> moveCost)
        {
            return positions.Select(p => moveCost(p, moveTo)).Sum();
        }

        private static IEnumerable<int> TestData() => new DataLoader("2021_CS", 7).ReadOneLineOfInts("DataTest.txt");
        private static IEnumerable<int> RealData() => new DataLoader("2021_CS", 7).ReadOneLineOfInts("DataReal.txt");
    }
}
