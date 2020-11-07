namespace AdventOfCode.Common
{
    internal static class Utils
    {
        public static long Lcm(long a, long b) => a * b / Gcd(a, b);
        public static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);

    }
}
