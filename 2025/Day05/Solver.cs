using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day05;

public static class Solver
{
    public static long Part1()
    {
        var (fresh, ingredients) = LoadData("Data.txt");
        return ingredients.Where(i => IsFresh(i, fresh)).Count();
    }

    public static long Part2()
    {
        var (fresh, _) = LoadData("Data.txt");
        var freshIntervals = fresh.ToList();
        while (TryMergeTwo(freshIntervals)) { }
        return freshIntervals.Select(i => i.Item2 - i.Item1 + 1).Sum();
    }

    private static bool TryMergeTwo(List<(long, long)> intervals)
    {
        for (var i = 0; i < intervals.Count - 1; i++)
        {
            for (var j = i + 1; j < intervals.Count; j++)
            {
                var i1 = intervals[i];
                var i2 = intervals[j];
                if ((i1.Item1 >= i2.Item1 && i1.Item1 <= i2.Item2) || (i1.Item2 >= i2.Item1 && i1.Item2 <= i2.Item2))
                {
                    intervals.RemoveAt(j);
                    intervals.RemoveAt(i);
                    intervals.Add((Math.Min(i1.Item1, i2.Item1), Math.Max(i1.Item2, i2.Item2)));
                    return true;
                }
            }
        }
        return false;
    }

    private static bool IsFresh(long ingredient, IEnumerable<(long, long)> fresh)
    {
        foreach(var f in fresh)
        {
            if (ingredient >= f.Item1 && ingredient <= f.Item2) 
            { 
                return true; 
            }
        }
        return false;
    }
    private static (IEnumerable<(long, long)>, IEnumerable<long>) LoadData(string fileName)
    {
        var fresh = new List<(long, long)>();
        var ingredients = new List<long>();
        var lines = new DataLoader(2025, 5).ReadStrings(fileName).ToList();
        var emptyAt = lines.FindIndex(s => s == "");
        foreach (var s in lines.Take(emptyAt))
        {
            var ns = s.Split('-');
            fresh.Add((long.Parse(ns[0]), long.Parse(ns[1])));
        }
        foreach (var s in lines.Skip(emptyAt+1))
        {
            ingredients.Add((long.Parse(s)));
        }
        return (fresh, ingredients);
    }

}
