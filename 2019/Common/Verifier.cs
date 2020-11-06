using System;

namespace AdventOfCode.Common
{
    internal static class Verifier
    {
        public static void Verify(int expected, int actual, string at)
        {
            if (expected != actual)
            {
                throw new Exception($"Expected {expected} but was {actual} at {at}");
            }
        }

        public static void Verify(long expected, long actual, string at)
        {
            if (expected != actual)
            {
                throw new Exception($"Expected {expected} but was {actual} at {at}");
            }
        }

        public static void Verify(string expected, string actual, string at)
        {
            if (expected != actual)
            {
                throw new Exception($"Expected {expected} but was {actual} at {at}");
            }
        }
    }
}
