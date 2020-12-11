using AdventOfCode.Common;
using AdventOfCode2020;
using System;

namespace AdventOfCode
{
    public static class Runner2020
    {
        public static void TestAll()
        {
            try
            {
                Console.WriteLine("Running tests for 2020");
                Verifier.Verify(55776, Day01.Part1(0), "day 1 part 1");
                Verifier.Verify(223162626, Day01.Part2(0), "day 1 part 2");
                Verifier.Verify(622, Day02.Part1(0), "day 2 part 1");
                Verifier.Verify(263, Day02.Part2(0), "day 2 part 1");
                Verifier.Verify(159, Day03.Part1(0), "day 3 part 1");
                Verifier.Verify(6419669520, Day03.Part2(0), "day 3 part 2");
                Verifier.Verify(210, Day04.Part1(0), "day 4 part 1");
                Verifier.Verify(131, Day04.Part2(0), "day 4 part 2");
                Verifier.Verify(894, Day05.Part1(0), "day 5 part 1");
                Verifier.Verify(579, Day05.Part2(0), "day 5 part 2");
                Verifier.Verify(6530, Day06.Part1(0), "day 6 part 1");
                Verifier.Verify(3323, Day06.Part2(0), "day 6 part 2");
                Verifier.Verify(192, Day07.Part1(0), "day 7 part 1");
                Verifier.Verify(12128, Day07.Part2(0), "day 7 part 2");
                Verifier.Verify(1671, Day08.Part1(0), "day 8 part 1");
                Verifier.Verify(892, Day08.Part2(0), "day 8 part 2");
                Verifier.Verify(70639851, Day09.Part1(0), "day 9 part 1");
                Verifier.Verify(8249240, Day09.Part2(0), "day 9 part 2");
                Verifier.Verify(2450, Day10.Part1(0), "day 10 part 1");
                Verifier.Verify(32396521357312, Day10.Part2(0), "day 10 part 2");
                Verifier.Verify(2273, Day11.Part1(0), "day 11 part 1");
                Verifier.Verify(2064, Day11.Part2(0), "day 11 part 2");
                Console.WriteLine("All tests for 2020 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
