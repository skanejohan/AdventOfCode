using AdventOfCode.Common;
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
            // Day 8: properly grouped:
            // 01100 11110 01100 00110 01100
            // 10010 00010 10010 00010 10010
            // 10010 00100 10000 00010 10000
            // 11110 01000 10000 00010 10000
            // 10010 10000 10010 10010 10010
            // 10010 11110 01100 01100 01100
            // Looks like "AZCJC" which is the correct answer

            var day8Expected = "011001111001100001100110010010000101001000010100101001000100100000001010000111100100010000000101000010010100001001010010100101001011110011000110001100";
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
                Verifier.Verify(422858, Day07.Part1(), "day 7 part 1");
                Verifier.Verify(14897241, Day07.Part2(), "day 7 part 2");
                Verifier.Verify(2440, Day08.Part1(), "day 8 part 1");
                Verifier.Verify(day8Expected, Day08.Part2(), "day 8 part 2");
                Verifier.Verify(3598076521, Day09.Part1(), "day 9 part 1");
                Verifier.Verify(90722, Day09.Part2(), "day 9 part 2");
                Verifier.Verify(286, Day10.Part1(), "day 10 part 1");
                Verifier.Verify(504, Day10.Part2(), "day 10 part 2");
                Console.WriteLine("All tests OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
