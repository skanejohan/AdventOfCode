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
                Verifier.Verify(138, Day01.Part1, "day 1 part 1");
                Verifier.Verify(1771, Day01.Part2, "day 1 part 2");
                Console.WriteLine("All tests for 2015 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
