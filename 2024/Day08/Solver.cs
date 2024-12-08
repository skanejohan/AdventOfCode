using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day08;

public static class Solver
{
    public static long Part1()
    {
        return Solve("Data.txt", 1, 1);
    }

    public static long Part2()
    {
        return Solve("Data.txt", -Math.Max(HighestRow, HighestCol), Math.Max(HighestRow, HighestCol));
    }

    static long Solve(string fileName, int minDistanceFactor, int maxDistanceFactor)
    {
        LoadData(fileName);
        var antinodes = new HashSet<(int, int)>();
        foreach (var (_, positions) in Antennas)
        {
            for (var i = 0; i < positions.Count - 1; i++)
            {
                for (var j = i + 1; j < positions.Count; j++)
                {
                    foreach (var antinode in GetAntinodes(positions[i], positions[j], minDistanceFactor, maxDistanceFactor))
                    {
                        antinodes.Add(antinode);
                    }
                }
            }
        }
        return antinodes.Count;
    }

    static IEnumerable<(int R, int C)> GetAntinodes((int R, int C) antenna1, (int R, int C) antenna2, int minDistanceFactor, int maxDistanceFactor)
    {
        var dRow = (antenna1.R - antenna2.R);
        var dCol = (antenna1.C - antenna2.C);
        for (var f = minDistanceFactor; f <= maxDistanceFactor; f++)
        {
            var candidate1 = (antenna2.R - dRow * f, antenna2.C - dCol * f);
            var candidate2 = (antenna1.R + dRow * f, antenna1.C + dCol * f);

            if (candidate1.Item1 >= 0 && candidate1.Item1 <= HighestRow && candidate1.Item2 >= 0 && candidate1.Item2 <= HighestCol)
            {
                yield return candidate1;
            }
            if (candidate2.Item1 >= 0 && candidate2.Item1 <= HighestRow && candidate2.Item2 >= 0 && candidate2.Item2 <= HighestCol)
            {
                yield return candidate2;
            }
        }
    }

    static void LoadData(string fileName)
    {
        Antennas = [];
        var lines = new DataLoader(2024, 8).ReadStrings(fileName).ToList();
        for (var row = 0; row < lines.Count; row++)
        {
            for (var col = 0; col < lines.First().Length; col++)
            {
                var c = lines[row][col];
                if (c != '.')
                {
                    if (!Antennas.TryGetValue(c, out var positions))
                    {
                        positions = new List<(int R, int C)>();
                        Antennas[c] = positions;
                    }
                    positions.Add((row, col));
                }
            }
        }
        HighestCol = lines.First().Length - 1;
        HighestRow = lines.Count - 1;
    }

    static Dictionary<char, List<(int R, int C)>> Antennas = [];
    static int HighestRow, HighestCol;
}
