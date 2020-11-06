using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Common
{
    internal static class DataReader
    {
        public static IEnumerable<int> ReadInts(string fileName)
        {
            foreach (var line in File.ReadLines(FullFileName(fileName)))
            {
                yield return int.Parse(line);
            }
        }

        public static IEnumerable<string> ReadStrings(string fileName)
            => File.ReadLines(FullFileName(fileName));

        public static IEnumerable<long> ReadCommaSeparatedLongList(string fileName)
            => File.ReadAllText(FullFileName(fileName)).Split(",").Select(long.Parse);

        public static string ReadAllText(string fileName)
            => File.ReadAllText(FullFileName(fileName));

        public static List<byte> ReadAllDigits(string fileName)
            => File.ReadAllText(FullFileName(fileName)).Select(c => c.ToString()).Select(c => byte.Parse(c)).ToList();

        private static string FullFileName(string fileName) 
            => Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Data\", fileName);
    }
}
