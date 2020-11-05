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
            try
            {
                Verifier.Verify(3339288, Day01.Part1(), "day 1 part 1");
                Verifier.Verify(5006064, Day01.Part2(), "day 1 part 2");
                Console.WriteLine("All tests OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
