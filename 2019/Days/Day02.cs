using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2019
{
    public static class Day02
    {
        public static long Part1()
        {
            var icc = new IntCodeComputer(GetData(12, 2), null, null);
            icc.Run();
            return icc.GetValueAt(0);
        }

        public static long Part2()
        {
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var icc = new IntCodeComputer(GetData(i, j), null, null);
                    icc.Run();
                    if (icc.GetValueAt(0) == 19690720)
                    {
                        return 100* i + j;
                    }
                }
            }
            return 0;
        }

        private static List<long> GetData(long noun, long verb)
        {
            var data = DataReader.ReadCommaSeparatedLongList("Day02Input.txt").ToList();
            data[1] = noun;
            data[2] = verb;
            return data;
        }
    }
}
