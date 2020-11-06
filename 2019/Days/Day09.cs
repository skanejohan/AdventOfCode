using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    internal static class Day09
    {
        public static long Part1() => RunWithInputInstruction(1);
        public static long Part2() => RunWithInputInstruction(2);

        private static long RunWithInputInstruction(int i)
        {
            long result = 0;
            var icc = new IntCodeComputer(new List<long>(GetData()), () => i, v =>
            {
                result = v;
                return true;
            });
            icc.Run();
            return result;
        }

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day09Input.txt").ToList();
    }
}
