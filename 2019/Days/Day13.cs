using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode.Days2019
{
    public static class Day13
    {
        public static long Part1()
        {
            long ctr = 0, result = 0;
            new IntCodeComputer(GetData(), null, v =>
            {
                if ((ctr + 1) % 3 == 0 && v == 2)
                {
                    result++;
                }
                ctr++;
                return true;
            }).Run();
            return result;
        }
         public static long Part2()
        {
            long ballX = 0;
            long paddleX = 0;
            long score = 0;
            int ctr = 0;

            long x = 0, y = 0;
            var data = GetData();
            data[0] = 2; // Play for free
            new IntCodeComputer(data,
                () => ballX == paddleX ? 0 : (ballX < paddleX ?  -1 : 1),
                v =>
                {
                    if (ctr == 0)
                    {
                        x = v;
                    }
                    else if (ctr == 1)
                    {
                        y = v;
                    }
                    else
                    {
                        if (x == -1 && y == 0)
                        {
                            score = v;
                        }
                        if (v == 4)
                        {
                            ballX = x;
                        }
                        if (v == 3)
                        {
                            paddleX = x;
                        }
                    }
                    ctr = (ctr + 1) % 3;
                    return true;
                }).Run();
            return score;
        }

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day13Input.txt").ToList();
    }
}
