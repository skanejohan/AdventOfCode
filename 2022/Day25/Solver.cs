using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Y2022.Day25
{
    public static class Solver
    {
        public static string Part1()
        {
            return $"{Snafu.FromLong(LoadData("data.txt").Select(Snafu.ToLong).Sum())}";
        }

        private static IEnumerable<string> LoadData(string fileName)
        {
            return new DataLoader(2022, 25).ReadStrings(fileName);
        }
    }

    internal static class Snafu
    {
        public static string FromLong(long n)
        {
            var s = "";
            for (var i = 24; i >= 0; i--)
            {
                var full = (long)Math.Pow(5, i);
                var half = full / 2;
                s += digitList[(int)(((n + half) / full) % 5)];
            }
            return s.TrimStart('0');
        }

        public static long ToLong(string snafu)
        {
            long result = 0;
            var s = snafu.Reverse().ToList();
            for (int i = 0; i < s.Count; i++)
            {
                result += digitDict[s[i]] * (long)Math.Pow(5, i);
            }
            return result;
        }

        private static List<char> digitList = new List<char> 
        { 
            '0', 
            '1', 
            '2', 
            '=', 
            '-' 
        };

        private static Dictionary<char, int> digitDict = new Dictionary<char, int>
        {
            { '=', -2 },
            { '-', -1 },
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
        };
    }
}
