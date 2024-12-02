using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day02;

public static class Solver
{
    public static long Part1()
    {
        return Solve("Data.txt", LineOk); 
    }

    public static long Part2()
    {
        return Solve("Data.txt", AnyLineOk);

        static bool AnyLineOk(List<int> line)
        {
            if (LineOk(line))
            {
                return true;
            }

            for (var i = 0; i < line.Count; i++)
            {
                var l = new List<int>(line);
                l.RemoveAt(i);
                if (LineOk(l))
                {
                    return true;
                }
            }

            return false;
        }
    }

    private static long Solve(string fileName, Func<List<int>, bool> lineOk)
    {
        var n = 0;
        foreach (var line in LoadData(fileName))
        {
            if (lineOk(line))
            {
                n++;
            }
        }
        return n;
    }

    private static bool LineOk(List<int> line)
    {
        var ok = true;
        Func<int, int, bool> fn = line[1] > line[0] ? IncreasingOk : DecreasingOk;

        for (int i = 1; i < line.Count; i++)
        {
            if (!fn(line[i - 1], line[i]))
            {
                ok = false;
            }
        }
        return ok;
    }

    private static IEnumerable<List<int>> LoadData(string fileName)
    {
        return new DataLoader(2024, 2).ReadStrings(fileName).Select(s => s.Split(' ').Select(int.Parse).ToList());
    }

    private static bool IncreasingOk(int a, int b)
    {
        return a < b && Math.Abs(a - b) < 4;
    }

    private static bool DecreasingOk(int a, int b)
    {
        return a > b && Math.Abs(a - b) < 4;
    }
}
