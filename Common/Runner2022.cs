using AdventOfCode.Common;
using System;

namespace AdventOfCode
{
    public static class Runner2022
    {
        public static void TestAll()
        {
            try
            {
                Console.WriteLine("Running tests for 2022");
                Verifier.Verify(69310, Y2022.Day01.Solver.Part1(), "day 1 part 1");
                Verifier.Verify(206104, Y2022.Day01.Solver.Part2(), "day 1 part 2");
                Verifier.Verify(13052, Y2022.Day02.Solver.Part1(), "day 2 part 1");
                Verifier.Verify(13693, Y2022.Day02.Solver.Part2(), "day 2 part 1");
                Verifier.Verify(8493, Y2022.Day03.Solver.Part1(), "day 3 part 1");
                Verifier.Verify(2552, Y2022.Day03.Solver.Part2(), "day 3 part 2");
                Verifier.Verify(571, Y2022.Day04.Solver.Part1(), "day 4 part 1");
                Verifier.Verify(917, Y2022.Day04.Solver.Part2(), "day 4 part 2");
                Verifier.Verify("WSFTMRHPP", Y2022.Day05.Solver.Part1(), "day 5 part 1");
                Verifier.Verify("GSLCMFBRP", Y2022.Day05.Solver.Part2(), "day 5 part 2");
                Verifier.Verify(1987, Y2022.Day06.Solver.Part1(), "day 6 part 1");
                Verifier.Verify(3059, Y2022.Day06.Solver.Part2(), "day 6 part 2");
                Verifier.Verify(2061777, Y2022.Day07.Solver.Part1(), "day 7 part 1");
                Verifier.Verify(4473403, Y2022.Day07.Solver.Part2(), "day 7 part 2");
                Verifier.Verify(1684, Y2022.Day08.Solver.Part1(), "day 8 part 1");
                Verifier.Verify(486540, Y2022.Day08.Solver.Part2(), "day 8 part 2");
                Verifier.Verify(6090, Y2022.Day09.Solver.Part1(), "day 9 part 1");
                Verifier.Verify(2566, Y2022.Day09.Solver.Part2(), "day 9 part 2");
                Verifier.Verify(15140, Y2022.Day10.Solver.Part1(), "day 10 part 1");
                Verifier.Verify("BPJAZGAP", Y2022.Day10.Solver.Part2(), "day 10 part 2");
                Verifier.Verify(58786, Y2022.Day11.Solver.Part1(), "day 11 part 1");
                Verifier.Verify(14952185856, Y2022.Day11.Solver.Part2(), "day 11 part 2");
                Verifier.Verify(380, Y2022.Day12.Solver.Part1(), "day 12 part 1");
                Verifier.Verify(375, Y2022.Day12.Solver.Part2(), "day 12 part 2");
                Verifier.Verify(5557, Y2022.Day13.Solver.Part1(), "day 13 part 1");
                Verifier.Verify(22425, Y2022.Day13.Solver.Part2(), "day 13 part 2");
                Verifier.Verify(757, Y2022.Day14.Solver.Part1(), "day 14 part 1");
                Verifier.Verify(24943, Y2022.Day14.Solver.Part2(), "day 14 part 2");
                Verifier.Verify(4725496, Y2022.Day15.Solver.Part1(), "day 15 part 1");
                Verifier.Verify(12051287042458, Y2022.Day15.Solver.Part2(), "day 15 part 2");
                Verifier.Verify(1716, Y2022.Day16.Solver.Part1(), "day 16 part 1");
                Verifier.Verify(2504, Y2022.Day16.Solver.Part2(), "day 16 part 2");
                Verifier.Verify(3168, Y2022.Day17.Solver.Part1(), "day 17 part 1");
                Verifier.Verify(1554117647070, Y2022.Day17.Solver.Part2(), "day 17 part 2");
                Verifier.Verify(3522, Y2022.Day18.Solver.Part1(), "day 18 part 1");
                Verifier.Verify(2074, Y2022.Day18.Solver.Part2(), "day 18 part 2");
                Verifier.Verify(1023, Y2022.Day19.Solver.Part1(), "day 19 part 1");
                Verifier.Verify(13520, Y2022.Day19.Solver.Part2(), "day 19 part 2");
                Verifier.Verify(16533, Y2022.Day20.Solver.Part1(), "day 20 part 1");
                Verifier.Verify(4789999181006, Y2022.Day20.Solver.Part2(), "day 20 part 2");
                Verifier.Verify(85616733059734, Y2022.Day21.Solver.Part1(), "day 21 part 1");
                Verifier.Verify(3560324848168, Y2022.Day21.Solver.Part2(), "day 21 part 2");
                Verifier.Verify(56372, Y2022.Day22.Solver.Part1(), "day 22 part 1");
                Verifier.Verify(0, Y2022.Day22.Solver.Part2(), "day 22 part 2");
                Verifier.Verify(3788, Y2022.Day23.Solver.Part1(), "day 23 part 1");
                Verifier.Verify(921, Y2022.Day23.Solver.Part2(), "day 23 part 2");
                Verifier.Verify(308, Y2022.Day24.Solver.Part1(), "day 24 part 1");
                Verifier.Verify(908, Y2022.Day24.Solver.Part2(), "day 24 part 2");
                Verifier.Verify("2---0-1-2=0=22=2-011", Y2022.Day25.Solver.Part1(), "day 25 part 1");
                Console.WriteLine("All tests for 2022 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
