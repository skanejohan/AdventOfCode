using AdventOfCode.Common;
using AdventOfCode2018;
using System;

namespace AdventOfCode
{
    public static class Runner2018
    {
        public static void TestAll()
        {
            try
            {
                Console.WriteLine("Running tests for 2018");
                Verifier.Verify(             435, Day01.Part1(), "day 1 part 1");
                Verifier.Verify(             245, Day01.Part2(), "day 1 part 2");
                Verifier.Verify(            6448, Day02.Part1(), "day 2 part 1");
                Verifier.Verify(0, Day02.Part2(), "day 2 part 1");
                Verifier.Verify(0, Day03.Part1(), "day 3 part 1");
                Verifier.Verify(0, Day03.Part2(), "day 3 part 2");
                Verifier.Verify(0, Day04.Part1(), "day 4 part 1");
                Verifier.Verify(0, Day04.Part2(), "day 4 part 2");
                Verifier.Verify(0, Day05.Part1(), "day 5 part 1");
                Verifier.Verify(0, Day05.Part2(), "day 5 part 2");
                Verifier.Verify(0, Day06.Part1(), "day 6 part 1");
                Verifier.Verify(0, Day06.Part2(), "day 6 part 2");
                Verifier.Verify(0, Day07.Part1(), "day 7 part 1");
                Verifier.Verify(0, Day07.Part2(), "day 7 part 2");
                Verifier.Verify(0, Day08.Part1(), "day 8 part 1");
                Verifier.Verify(0, Day08.Part2(), "day 8 part 2");
                Verifier.Verify(0, Day09.Part1(), "day 9 part 1");
                Verifier.Verify(0, Day09.Part2(), "day 9 part 2");
                Verifier.Verify(0, Day10.Part1(), "day 10 part 1");
                Verifier.Verify(0, Day10.Part2(), "day 10 part 2");
                Verifier.Verify(0, Day11.Part1(), "day 11 part 1");
                Verifier.Verify(0, Day11.Part2(), "day 11 part 2");
                Verifier.Verify(0, Day12.Part1(), "day 12 part 1");
                Verifier.Verify(0, Day12.Part2(), "day 12 part 2");
                Verifier.Verify(0, Day13.Part1(), "day 13 part 1");
                Verifier.Verify(0, Day13.Part2(), "day 13 part 2");
                Verifier.Verify(0, Day14.Part1(), "day 14 part 1");
                Verifier.Verify(0, Day14.Part2(), "day 14 part 2");
                Verifier.Verify(0, Day15.Part1(), "day 15 part 1");
                Verifier.Verify(0, Day15.Part2(), "day 15 part 2");
                Verifier.Verify(0, Day16.Part1(), "day 16 part 1");
                Verifier.Verify(0, Day16.Part2(), "day 16 part 2");
                Verifier.Verify(0, Day17.Part1(), "day 17 part 1");
                Verifier.Verify(0, Day17.Part2(), "day 17 part 2");
                Verifier.Verify(0, Day18.Part1(), "day 18 part 1");
                Verifier.Verify(0, Day18.Part2(), "day 18 part 2");
                Verifier.Verify(0, Day19.Part1(), "day 19 part 1");
                Verifier.Verify(0, Day19.Part2(), "day 19 part 2");
                Verifier.Verify(0, Day20.Part1(), "day 20 part 1");
                Verifier.Verify(0, Day20.Part2(), "day 20 part 2");
                Verifier.Verify(0, Day21.Part1(), "day 21 part 1");
                Verifier.Verify(0, Day21.Part2(), "day 21 part 2");
                Verifier.Verify(0, Day22.Part1(), "day 22 part 1");
                Verifier.Verify(0, Day22.Part2(), "day 22 part 2");
                Verifier.Verify(0, Day23.Part1(), "day 23 part 1");
                Verifier.Verify(0, Day23.Part2(), "day 23 part 2");
                Verifier.Verify(0, Day24.Part1(), "day 24 part 1");
                Verifier.Verify(0, Day24.Part2(), "day 24 part 2");
                Verifier.Verify(0, Day25.Part1(), "day 25 part 1");
                Console.WriteLine("All tests for 2018 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
