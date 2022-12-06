using CSharpLib;
using CSharpLib.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day06
{
    public static class Solver
    {
        public static long Part1()
        {
            return Find(LoadData("data.txt"), 4);
        }

        public static long Part2()
        {
            return Find(LoadData("data.txt"), 14);
        }

        public static long Find(string s, int count)
        {
            var cs = new CountedSet<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (i >= count)
                {
                    cs.Remove(s[i - count]);
                }
                cs.Add(s[i]);
                if (cs.Count() == count)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        private static string LoadData(string fileName)
        {
            return new DataLoader(2022, 6).ReadStrings(fileName).First();
        }
    }
}
