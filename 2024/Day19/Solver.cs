using CSharpLib;
using CSharpLib.Algorithms;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day19;

public static class Solver
{
    public static long Part1()
    {
        LoadData("data.txt");
        return designs.Where(d => Solve(d, 0, PatternsAtIndex(d))).Count();
    }

    public static long Part2()
    {
        var n = 0L;
        LoadData("data.txt");
        foreach (var d in designs)
        {
            var s = NumberOfSolutions(d);
            n += s;
        }

        long NumberOfSolutions(string design)
        {
            Dictionary<int, long> calculatedSolutions = [];
            var patternsAtIndex = PatternsAtIndex(design);
            return Sol(0);

            long Sol(int i)
            {
                if (calculatedSolutions.TryGetValue(i, out var value))
                {
                    return value;
                }
                var n = 0L;
                foreach (var pattern in patternsAtIndex[i])
                {
                    if (i + pattern.Length == design.Length)
                    {
                        n++;
                    }
                    else
                    {
                        n += Sol(i + pattern.Length);
                    }
                }
                calculatedSolutions[i] = n;
                return n;
            }
        }
        return n;
    }

    static bool Solve(string design, int startIndex, Dictionary<int, List<string>> patternsAtIndex)
    {
        var targetIndex = design.Length;
        try
        {
            var solution = Dijkstra<int>.Solve(startIndex, getNeighbors, i => i == targetIndex);
            return true;
        }
        catch
        {
            return false;
        }

        IEnumerable<(int, long)> getNeighbors(int index)
        {
            foreach (var p in patternsAtIndex[index])
            {
                if (design.IndexOf(p, index) == index)
                {
                    yield return (index + p.Length, 1);
                }
            }
        }
    }

    private static Dictionary<int, List<string>> PatternsAtIndex(string design)
    {
        var dictionary = new Dictionary<int, List<string>>();
        for (int i = 0; i <= design.Length; i++)
        {
            dictionary[i] = [];
            foreach (var p in patterns)
            {
                if (design.IndexOf(p, i) == i) // The pattern exists at index i
                {
                    dictionary[i].Add(p);
                }
            }
        }
        return dictionary;
    }

    private static void LoadData(string fileName)
    {
        var data = new DataLoader(2024, 19).ReadStrings(fileName).ChunkBy(s => s == "").ToList();
        patterns = data[0][0].Split(", ").OrderBy(p => -p.Length).ToList();
        designs = data[1];
    }

    static List<string> patterns = [];
    static List<string> designs = [];
}
