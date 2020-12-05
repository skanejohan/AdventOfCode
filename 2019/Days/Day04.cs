using System.Linq;

namespace AdventOfCode.Days2019
{
    public static class Day04
    {
        public static int Part1() => result1;
        public static int Part2() => result2;

        static Day04()
        {
            for (var i = 172930; i <= 683082; i++)
            {
                var s = i.ToString();
                if (Passes(s))
                {
                    result1++;
                    if (HasTwoInARow(s))
                    {
                        result2++;
                    }
                }
            }
        }

        private static int result1;
        private static int result2;

        private static bool Passes(string number)
        {
            var ok = false;
            var lastN = -1;
            foreach (var n in number.Select(c => c.ToString()).Select(int.Parse))
            {
                if (n == lastN)
                {
                    ok = true;
                }
                if (n < lastN)
                {
                    return false;
                }
                lastN = n;
            }
            return ok;

        }

        private static bool HasTwoInARow(string number)
        {
            var cond1 = number[0] == number[1] && number[1] != number[2];
            var cond2 = number[0] != number[1] && number[1] == number[2] && number[2] != number[3];
            var cond3 = number[1] != number[2] && number[2] == number[3] && number[3] != number[4];
            var cond4 = number[2] != number[3] && number[3] == number[4] && number[4] != number[5];
            var cond5 = number[3] != number[4] && number[4] == number[5];
            return cond1 || cond2 || cond3 || cond4 || cond5;
        }
    }
}
