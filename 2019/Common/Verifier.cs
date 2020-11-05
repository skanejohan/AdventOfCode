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
    }
}
