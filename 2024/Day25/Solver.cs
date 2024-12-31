using CSharpLib;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day25;

public static class Solver
{
    public static long Part1()
    {
        var n = 0L;
        var (Locks, Keys) = LoadData("data.txt");
        foreach(var l in Locks)
        {
            foreach(var k in Keys)
            {
                var x = l.Zip(k, (ln, kn) => ln + kn).ToList();
                if (l.Zip(k, (ln, kn) => ln + kn).All(n => n <= 5))
                {
                    n++;
                }
            }
        }
        return n;
    }

    public static long Part2()
    {
        return 0;
    }

    private static (List<List<int>> Locks, List<List<int>> Keys) LoadData(string fileName)
    {
        List<List<int>> keys = [];
        List<List<int>> locks = [];

        foreach (var chunk in new DataLoader(2024, 25).ReadStrings(fileName).ChunkBy(string.IsNullOrEmpty))
        {
            if (chunk.First().IndexOf('#') == -1)
            {
                List<int> key = [0, 0, 0, 0, 0];
                for (var row = 0; row < chunk.Count; row++)
                {
                    for (var col = 0; col < chunk[row].Length; col++)
                    {
                        if (chunk[row][col] == '.')
                        {
                            key[col] = 5 - row;
                        }
                    }
                }
                keys.Add(key);
            }
            else
            {
                List<int> lck = [0, 0, 0, 0, 0];
                for (var row = 0; row < chunk.Count; row++)
                {
                    for (var col = 0; col < chunk[row].Length; col++)
                    {
                        if (chunk[row][col] == '#')
                        {
                            lck[col] = row;
                        }
                    }
                }
                locks.Add(lck);
            }
        }

        return (locks, keys);
    }
}
