using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day02;

public static class Solver
{
    public static long Part1()
    {
        var total = 0L;
        var pairs = LoadData("Data.txt");
        foreach(var pair in pairs)
        {
            for(var i = pair.Item1; i <= pair.Item2; i++)
            {
                if (IsRepeatedPart1(i))
                {
                    total += i;
                }
            }
        }
        return total;
    }

    public static long Part2()
    {
        var total = 0L;
        var pairs = LoadData("Data.txt");
        foreach (var pair in pairs)
        {
            for (var i = pair.Item1; i <= pair.Item2; i++)
            {
                if (IsRepeatedPart2(i))
                {
                    total += i;
                }
            }
        }
        return total;
    }

    private static IEnumerable<(long, long)> LoadData(string fileName)
    {
        var line = new DataLoader(2025, 2).ReadStrings(fileName).First();
        return line.Split(',').Select(pair => (long.Parse(pair.Split('-')[0]), long.Parse(pair.Split('-')[1])));
    }

    private static bool IsRepeatedPart1(long n)
    {
        var s = n.ToString();
        if (s.Length % 2 == 1)
        {
            return false;
        }
        var pt1 = s[..(s.Length / 2)];
        var pt2 = s[(s.Length / 2)..];
        return pt1 == pt2;
    }

    private static bool IsRepeatedPart2(long n)
    {
        var s = n.ToString();
        for (var chunkSize = 1; chunkSize <= (s.Length / 2); chunkSize++)
        {
            if (s.Length % chunkSize != 0)
            {
                continue;
            }

            var parts = Enumerable.Range(0, s.Length / chunkSize).Select(i => s.Substring(i * chunkSize, chunkSize));
            if (AllPartsAreTheSame(parts))
            {
                return true;
            }
        }
        return false;

        static bool AllPartsAreTheSame(IEnumerable<string> parts)
        {
            foreach (var part in parts)
            {
                if (part != parts.First())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
