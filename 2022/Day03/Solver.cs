using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day03
{
    public static class Solver
    {
        public static long Part1()
        {
            return LoadData("data.txt")
                .Select(s => (s[..(s.Length / 2)], s[(s.Length / 2)..]))
                .Select(s => CommonCharacter(s.Item1, s.Item2))
                .Select(Cost)
                .Sum();
        }

        public static long Part2()
        {
            return LoadData("data.txt")
                .Chunk(3)
                .Select(chunk => CommonCharacter(chunk[0], chunk[1], chunk[2]))
                .Select(Cost)
                .Sum();
        }

        private static int Cost(char c)
        {
            return c > 96 ? c - 96 : c - 38;
        }

        private static char CommonCharacter(string s1, string s2)
        {
            return (new HashSet<char>(s1)).Intersect(new HashSet<char>(s2)).First();
        }

        private static char CommonCharacter(string s1, string s2, string s3)
        {
            return (new HashSet<char>(s1)).Intersect(new HashSet<char>(s2)).Intersect(new HashSet<char>(s3)).First();
        }

        private static IEnumerable<string> LoadData(string fileName)
        {
            return new DataLoader(2022, 3).ReadStrings(fileName);
        }
    }
}
