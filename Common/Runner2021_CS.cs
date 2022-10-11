using AdventOfCode.Common;
using _2021_CS;
using System;

namespace AdventOfCode
{
    public static class Runner2021_CS
    {
        public static void TestAll()
        {
            try
            {
                Console.WriteLine("Running tests for 2021");
                Verifier.Verify(1529, Day01.Part1(), "day 1 part 1");
                Verifier.Verify(1567, Day01.Part2(), "day 1 part 2");
                Verifier.Verify(1813801, Day02.Part1(), "day 2 part 1");
                Verifier.Verify(1960569556, Day02.Part2(), "day 2 part 1");
                Verifier.Verify(2724524, Day03.Part1(), "day 3 part 1");
                Verifier.Verify(2775870, Day03.Part2(), "day 3 part 2");
                Verifier.Verify(44736, Day04.Part1(), "day 4 part 1");
                Verifier.Verify(1827, Day04.Part2(), "day 4 part 2");
                Verifier.Verify(4728, Day05.Part1(), "day 5 part 1");
                Verifier.Verify(17717, Day05.Part2(), "day 5 part 2");
                Verifier.Verify(375482, Day06.Part1(), "day 6 part 1");
                Verifier.Verify(1689540415957, Day06.Part2(), "day 6 part 2");
                Verifier.Verify(356922, Day07.Part1(), "day 7 part 1");
                Verifier.Verify(100347031, Day07.Part2(), "day 7 part 2");
                Verifier.Verify(488, Day08.Part1(), "day 8 part 1");
                Verifier.Verify(1040429, Day08.Part2(), "day 8 part 2");
                Verifier.Verify(496, Day09.Part1(), "day 9 part 1");
                Verifier.Verify(902880, Day09.Part2(), "day 9 part 2");
                Verifier.Verify(294195, Day10.Part1(), "day 10 part 1");
                Verifier.Verify(3490802734, Day10.Part2(), "day 10 part 2");
                Verifier.Verify(1725, Day11.Part1(), "day 11 part 1");
                Verifier.Verify(308, Day11.Part2(), "day 11 part 2");
                Verifier.Verify(4338, Day12.Part1(), "day 12 part 1");
                Verifier.Verify(114189, Day12.Part2(), "day 12 part 2");
                Verifier.Verify(661, Day13.Part1(), "day 13 part 1");
                Verifier.Verify("PFKLKCFP", Day13.Part2(), "day 13 part 2");
                Verifier.Verify(2975, Day14.Part1(), "day 14 part 1");
                Verifier.Verify(3015383850689, Day14.Part2(), "day 14 part 2");
                Verifier.Verify(393, Day15.Part1(), "day 15 part 1");
                Verifier.Verify(2823, Day15.Part2(), "day 15 part 2");
                Verifier.Verify(913, Day16.Part1(), "day 16 part 1");
                Verifier.Verify(1510977819698, Day16.Part2(), "day 16 part 2");
                Verifier.Verify(9180, Day17.Part1(), "day 17 part 1");
                Verifier.Verify(3767, Day17.Part2(), "day 17 part 2");
                Verifier.Verify(3892, _2021_CS.Day18.Solver.Part1(), "day 18 part 1");
                Verifier.Verify(4909, _2021_CS.Day18.Solver.Part2(), "day 18 part 2");
                Verifier.Verify(396, _2021_CS.Day19.Solver.Part1(), "day 19 part 1");
                Verifier.Verify(11828, _2021_CS.Day19.Solver.Part2(), "day 19 part 2");
                Verifier.Verify(4917, _2021_CS.Day20.Solver.Part1(), "day 20 part 1");
                Verifier.Verify(16389, _2021_CS.Day20.Solver.Part2(), "day 20 part 2");
                //Verifier.Verify(          908091, Day21.Part1(), "day 21 part 1");
                //Verifier.Verify( 190897246590017, Day21.Part2(), "day 21 part 2");
                //Verifier.Verify(          591365, Day22.Part1(), "day 22 part 1");
                //Verifier.Verify(1211172281877240, Day22.Part2(), "day 22 part 2");
                //Verifier.Verify(           10607, Day23.Part1(), "day 23 part 1");
                ////Verifier.Verify(0, Day23.Part2(), "day 23 part 2");
                //Verifier.Verify(  39999698799429, Day24.Part1(), "day 24 part 1");
                //Verifier.Verify(  18116121134117, Day24.Part2(), "day 24 part 2");
                //Verifier.Verify(             386, Day25.Part1(), "day 25 part 1");
                Console.WriteLine("All tests for 2021 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
