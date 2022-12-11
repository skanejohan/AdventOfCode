namespace CSharpLib.Utils
{
    public static class MathUtils
    {
        // Least Common Multiple (LCM) of two integers is the smallest integer that is a
        // multiple of both numbers.
        public static long Lcm(long a, long b) => a * b / Gcd(a, b);

        // Greatest Common Divisor (GCD) of two positive integers is the largest positive
        // integer that divides both numbers without remainder. It is useful for reducing
        // fractions to be in its lowest terms.
        public static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);
    }
}
