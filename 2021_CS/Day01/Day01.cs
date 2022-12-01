using CSharpLib;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day01
    {
        public static int Part1() => Data().NoOfIncreases();

        public static int Part2() => Data().SlidingWindow3((a, b, c) => a + b + c).ToList().NoOfIncreases();

        private static List<int> Data() => new DataLoader("2021_CS", 1).ReadInts("DataReal.txt").ToList();
    }
}
