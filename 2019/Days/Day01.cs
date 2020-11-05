using AdventOfCode.Common;
using System;
using System.Linq;

namespace AdventOfCode.Days
{
    internal static class Day01
    {
        private static int FuelRequired(int mass) => (int)Math.Floor((double)(mass / 3 - 2));

        private static int FuelRequiredRecursively(int mass)
        {
            return fn(mass, 0);

            int fn(int mass, int aggregate)
            {
                if (mass == 0)
                {
                    return aggregate;
                }

                var fuel = Math.Max(FuelRequired(mass), 0);
                return fn(fuel, aggregate + fuel);
            } 
        }

        public static int Part1() => DataReader.ReadInts("Day01Input.txt").Select(FuelRequired).Sum();

        public static int Part2() => DataReader.ReadInts("Day01Input.txt").Select(FuelRequiredRecursively).Sum();
    }
}
