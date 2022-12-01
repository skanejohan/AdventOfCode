using CSharpLib;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day01
{
    public static class Solver
    {
        public static long Part1()
        {
            return LoadData("Data.txt").Select(l => l.Sum()).Max();
        }

        public static long Part2()
        {
            var data = LoadData("Data.txt").Select(l => l.Sum()).OrderByDescending(i => i).ToList();
            return data[0] + data[1] + data[2];
        }

        private static IEnumerable<IEnumerable<int>> LoadData(string fileName)
        {
            return new DataLoader(2022, 1).ReadStrings(fileName).ChunkBy(string.IsNullOrEmpty).Select(l => l.Select(int.Parse));
        }
    }
}
