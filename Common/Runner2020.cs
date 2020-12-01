﻿using AdventOfCode.Common;
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
                Console.WriteLine("All tests for 2020 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
