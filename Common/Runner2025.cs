using AdventOfCode.Common;
using System;

namespace AdventOfCode;

public static class Runner2025
{
    public static void TestAll()
    {
        try
        {
            Console.WriteLine("Running tests for 2025");
            Verifier.Verify(1165, Y2025.Day01.Solver.Part1(), "day 01 part 1");
            Verifier.Verify(6496, Y2025.Day01.Solver.Part2(), "day 01 part 2");
            Verifier.Verify(22062284697, Y2025.Day02.Solver.Part1(), "day 02 part 1");
            Verifier.Verify(46666175279, Y2025.Day02.Solver.Part2(), "day 02 part 2");
            Verifier.Verify(0, Y2025.Day03.Solver.Part1(), "day 03 part 1");
            Verifier.Verify(0, Y2025.Day03.Solver.Part2(), "day 03 part 2");
            Verifier.Verify(0, Y2025.Day04.Solver.Part1(), "day 04 part 1");
            Verifier.Verify(0, Y2025.Day04.Solver.Part2(), "day 04 part 2");
            Verifier.Verify(0, Y2025.Day05.Solver.Part1(), "day 05 part 1");
            Verifier.Verify(0, Y2025.Day05.Solver.Part2(), "day 05 part 2");
            Verifier.Verify(0, Y2025.Day06.Solver.Part1(), "day 06 part 1");
            Verifier.Verify(0, Y2025.Day06.Solver.Part2(), "day 06 part 2");
            Verifier.Verify(0, Y2025.Day07.Solver.Part1(), "day 07 part 1");
            Verifier.Verify(0, Y2025.Day07.Solver.Part2(), "day 07 part 2");
            Verifier.Verify(0, Y2025.Day08.Solver.Part1(), "day 08 part 1");
            Verifier.Verify(0, Y2025.Day08.Solver.Part2(), "day 08 part 2");
            Verifier.Verify(0, Y2025.Day09.Solver.Part1(), "day 09 part 1");
            Verifier.Verify(0, Y2025.Day09.Solver.Part2(), "day 09 part 2");
            Verifier.Verify(0, Y2025.Day10.Solver.Part1(), "day 10 part 1");
            Verifier.Verify(0, Y2025.Day10.Solver.Part2(), "day 10 part 2");
            Verifier.Verify(0, Y2025.Day11.Solver.Part1(), "day 11 part 1");
            Verifier.Verify(0, Y2025.Day11.Solver.Part2(), "day 11 part 2");
            Verifier.Verify(0, Y2025.Day12.Solver.Part1(), "day 12 part 1");
            Verifier.Verify(0, Y2025.Day12.Solver.Part2(), "day 12 part 2");
            Console.WriteLine("All tests for 2025 OK");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
