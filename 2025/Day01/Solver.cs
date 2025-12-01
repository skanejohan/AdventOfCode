using CSharpLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day01;

public static class Solver
{
    public static long Part1()
    {
        var n = 0;
        var pos = 50;
        foreach(var (L, N) in LoadData("Data.txt"))
        {
            pos = L ? (pos + 100 - N) % 100 : (pos + 100 + N) % 100;
            if (pos == 0)
            {
                n++;
            }
        }
        return n;
    }

    public static long Part2()
    {
        var n = 0;
        var pos = 50;
        foreach (var (L, N) in LoadData("Data.txt"))
        {
            var oldPos = pos;
            var fullLaps = N / 100;
            var movement = N % 100;
            pos = L ? pos - movement : pos + movement;

            n += fullLaps;
            if (pos == 0 || pos == 100 || (oldPos < 100 && pos > 100) || (oldPos > 0 && pos < 0))
            {
                n++;
            }
            pos = (pos + 100) % 100;
        }
        return n;
    }

    private static IEnumerable<(bool L,int N)> LoadData(string fileName)
    {
        return new DataLoader(2025, 1).ReadStrings(fileName).Select(l => (l[0] == 'L', int.Parse(l[1..])));
    }
}
