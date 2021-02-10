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
                Verifier.Verify(1710, Day12.Part1(0), "day 12 part 1");
                Verifier.Verify(62045, Day12.Part2(0), "day 12 part 2");
                Verifier.Verify(119, Day13.Part1(), "day 13 part 1");
                Verifier.Verify(1106724616194525, Day13.Part2(), "day 13 part 2");
                Verifier.Verify(5875750429995, Day14.Part1(0), "day 14 part 1");
                Verifier.Verify(5272149590143, Day14.Part2(0), "day 14 part 2");
                Verifier.Verify(959, Day15.Part1(0), "day 15 part 1");
                Verifier.Verify(116590, Day15.Part2(0), "day 15 part 2");
                Verifier.Verify(25972, Day16.Part1(0), "day 16 part 1");
                Verifier.Verify(622670335901, Day16.Part2(0), "day 16 part 2");
                Verifier.Verify(353, Day17.Part1(0), "day 17 part 1");
                Verifier.Verify(2472, Day17.Part2(0), "day 17 part 2");
                Verifier.Verify(11076907812171, Day18.Part1(), "day 18 part 1");
                Verifier.Verify(283729053022731, Day18.Part2(), "day 18 part 2");
                Verifier.Verify(149, Day19.Part1(), "day 19 part 1");
                Verifier.Verify(332, Day19.Part2(), "day 19 part 2");
                Verifier.Verify(83775126454273, Day20.Part1(), "day 20 part 1");
                //TODO Verifier.Verify(, Day20.Part2(), "day 20 part 2");
                Verifier.Verify(2659, Day21.Part1(), "day 21 part 1");
                Verifier.Verify("rcqb,cltx,nrl,qjvvcvz,tsqpn,xhnk,tfqsb,zqzmzl", Day21.Part2(), "day 21 part 2");
                Verifier.Verify(31781, Day22.Part1(), "day 22 part 1");
                Verifier.Verify(35154, Day22.Part2(), "day 22 part 2");
                Verifier.Verify(75893264, Day23.Part1(), "day 23 part 1");
                Verifier.Verify(38162588308, Day23.Part2(), "day 23 part 2");
                Verifier.Verify(228, Day24.Part1(), "day 24 part 1");
                Verifier.Verify(3672, Day24.Part2(), "day 24 part 2");
                Verifier.Verify(181800, Day25.Part1(), "day 25 part 1");
                //TODO Verifier.Verify(, Day25.Part2(), "day 25 part 2");
                Console.WriteLine("All tests for 2020 OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
