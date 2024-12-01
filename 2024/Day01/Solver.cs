using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day01;

public static class Solver
{
    public static long Part1()
    {
        var (fstRow, sndRow) = LoadData();
        var sum = 0L;
        for (var i = 0; i < fstRow.Count; i++)
        {
            sum += Math.Abs(fstRow[i] - sndRow[i]);
        }
        return sum;
    }

    public static long Part2()
    {
        var (fstRow, sndRow) = LoadData();
        var appearances = new Dictionary<long, long>();
        foreach(var l in sndRow)
        {
            if (!appearances.TryGetValue(l, out var value))
            {
                value = 0;
            }
            appearances[l] = value + 1;
        }
        var sum = 0L;
        foreach (var value in fstRow)
        {
            var mult = appearances.TryGetValue(value, out var m) ? m : 0;
            sum += value * mult;
        }
        return sum;
    }

    private static (List<long> First, List<long> Second) LoadData(string fileName = "Data.txt")
    {
        var values = new DataLoader(2024, 1).ReadStrings(fileName).Select(s => s.Split("   ").Select(long.Parse)).Select(l => (l.First(), l.Skip(1).First()));
        var fstRow = values.Select(i => i.Item1).Order().ToList();
        var sndRow = values.Select(i => i.Item2).Order().ToList();
        return (fstRow, sndRow);
    }
}
