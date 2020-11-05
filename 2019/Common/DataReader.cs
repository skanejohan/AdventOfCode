using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Common
{
    internal static class DataReader
    {
        public static IEnumerable<int> ReadInts(string fileName)
        {
            var fileNameWithPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Data\", fileName);
            foreach(var line in File.ReadLines(fileNameWithPath))
            {
                yield return int.Parse(line);
            }
        }
    }
}
