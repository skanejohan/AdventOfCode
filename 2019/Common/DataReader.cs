using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Common
{
    internal static class DataReader
    {
        public static IEnumerable<int> ReadInts(string fileName)
        {
            foreach(var line in File.ReadLines(FullFileName(fileName)))
            {
                yield return int.Parse(line);
            }
        }

        public static IEnumerable<long> ReadCommaSeparatedLongList(string fileName)
        {
            return File.ReadAllText(FullFileName(fileName)).Split(",").Select(long.Parse);
        }

        private static string FullFileName(string fileName) => Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Data\", fileName);
    }
}
