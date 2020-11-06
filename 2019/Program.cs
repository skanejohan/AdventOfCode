﻿using AdventOfCode.Common;
using AdventOfCode.Days;
using System;

namespace AdventOfCode
{
    class Program
    {
        public static void Main(string[] args)
        {
            TestAll();
        }

        private static void TestAll()
        {
            try
            {
                Verifier.Verify(3339288, Day01.Part1(), "day 1 part 1");
                Verifier.Verify(5006064, Day01.Part2(), "day 1 part 2");
                Verifier.Verify(2692315, Day02.Part1(), "day 2 part 1");
                Verifier.Verify(9507, Day02.Part2(), "day 2 part 2");
                Verifier.Verify(489, Day03.Part1(), "day 3 part 1");
                Verifier.Verify(93654, Day03.Part2(), "day 3 part 2");
                Verifier.Verify(1675, Day04.Part1(), "day 4 part 1");
                Verifier.Verify(1142, Day04.Part2(), "day 4 part 2");
                Verifier.Verify(4601506, Day05.Part1(), "day 5 part 1");
                Verifier.Verify(5525561, Day05.Part2(), "day 5 part 2");
                Verifier.Verify(110190, Day06.Part1(), "day 6 part 1");
                Verifier.Verify(343, Day06.Part2(), "day 6 part 2");
                Console.WriteLine("All tests OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
