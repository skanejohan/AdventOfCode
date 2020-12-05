using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days2019
{
    public static class Day05
    {
        public static long Part1()
        {
            long result = 0;
            var icc = new IntCodeComputer(GetData(), () => 1, v => { result = v; return true; });
            icc.Run();
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            var icc = new IntCodeComputer(GetData(), () => 5, v => { result = v; return true; });
            icc.Run();
            return result;
        }

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day05Input.txt").ToList();
    }
}
