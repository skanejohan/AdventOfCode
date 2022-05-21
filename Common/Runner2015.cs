using AdventOfCode.Common;
using AdventOfCode2015;
using System;

namespace AdventOfCode
{
    public static class Runner2015
    {
        public static void TestAll()
        {
            try
            {
                Console.WriteLine("Running tests for 2015");
                Verifier.Verify(138, Day01.Part1(0), "day 1 part 1");
                Verifier.Verify(1771, Day01.Part2(0), "day 1 part 2");
                Verifier.Verify(1598415, Day02.Part1(0), "day 2 part 1");
                Verifier.Verify(3812909, Day02.Part2(0), "day 2 part 2");
                Verifier.Verify(2565, Day03.Part1(0), "day 3 part 1");
                Verifier.Verify(2639, Day03.Part2(0), "day 3 part 2");
                Verifier.Verify(282749, Day04.Part1(0), "day 4 part 1");
                Verifier.Verify(9962624, Day04.Part2(0), "day 4 part 2");
                Verifier.Verify(238, Day05.Part1(), "day 5 part 1");
                Verifier.Verify(69, Day05.Part2 (), "day 5 part 2");
                Verifier.Verify(569999, Day06.Part1 (), "day 6 part 1");
                Verifier.Verify(17836115, Day06.Part2 (), "day 6 part 2");
                Verifier.Verify(3176, Day07.Part1(), "day 7 part 1");
                Console.WriteLine("All tests for 2015 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
