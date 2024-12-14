using System;

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

        // Tries to solve a system consisting of two linear equations using the addition method,
        // see https://courses.lumenlearning.com/wmopen-collegealgebra/chapter/introduction-systems-of-linear-equations-two-variables/
        //
        // Equation 1: ax + by = c
        // Equation 2: dx + ey = f
        public static bool TrySolve(long a, long b, long c, long d, long e, long f, out (long X, long Y) result)
        {
            var lcm = Lcm(a, d);
            var mul1 = Math.Sign(a) == Math.Sign(d) ? -lcm / a : lcm / a;
            var mul2 = lcm / d;
            var b2 = b * mul1;
            var c2 = c * mul1;
            var e2 = e * mul2;
            var f2 = f * mul2;
            result.Y = (c2 + f2) / (b2 + e2);
            result.X = (c - b * result.Y) / a;
            return a * result.X + b * result.Y == c && d * result.X + e * result.Y == f;
        }
    }
}
