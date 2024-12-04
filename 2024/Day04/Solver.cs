using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day04;

public static class Solver
{
    public static long Part1()
    {
        var n = 0;
        LoadData("Data.txt");
        foreach(var (R, C) in Xs)
        {
            if (Ms.Contains((R + 1, C)) && As.Contains((R + 2, C)) && Ss.Contains((R + 3, C))) // Down
            {
                n++;
            }
            if (Ms.Contains((R - 1, C)) && As.Contains((R - 2, C)) && Ss.Contains((R - 3, C))) // Up
            {
                n++;
            }
            if (Ms.Contains((R, C + 1)) && As.Contains((R, C + 2)) && Ss.Contains((R, C + 3))) // Right
            {
                n++;
            }
            if (Ms.Contains((R, C - 1)) && As.Contains((R, C - 2)) && Ss.Contains((R, C - 3))) // Left
            {
                n++;
            }
            if (Ms.Contains((R + 1, C + 1)) && As.Contains((R + 2, C + 2)) && Ss.Contains((R + 3, C + 3))) // DownRight
            {
                n++;
            }
            if (Ms.Contains((R - 1, C - 1)) && As.Contains((R - 2, C - 2)) && Ss.Contains((R - 3, C - 3))) // UpLeft
            {
                n++;
            }
            if (Ms.Contains((R - 1, C + 1)) && As.Contains((R - 2, C + 2)) && Ss.Contains((R - 3, C + 3))) // UpRight
            {
                n++;
            }
            if (Ms.Contains((R + 1, C - 1)) && As.Contains((R + 2, C - 2)) && Ss.Contains((R + 3, C - 3))) // DownLeft
            {
                n++;
            }
        }
        return n;
    }

    public static long Part2()
    {
        var n = 0;
        LoadData("Data.txt");
        foreach (var (R, C) in As)
        {
            if (Ms.Contains((R - 1, C - 1)) && Ss.Contains((R + 1, C + 1)) && Ms.Contains((R - 1, C + 1)) && Ss.Contains((R + 1, C - 1))) 
            {
                n++;
            }
            if (Ms.Contains((R - 1, C - 1)) && Ss.Contains((R + 1, C + 1)) && Ms.Contains((R + 1, C - 1)) && Ss.Contains((R - 1, C + 1)))
            {
                n++;
            }
            if (Ms.Contains((R + 1, C - 1)) && Ss.Contains((R - 1, C + 1)) && Ms.Contains((R + 1, C + 1)) && Ss.Contains((R - 1, C - 1)))
            {
                n++;
            }
            if (Ms.Contains((R - 1, C + 1)) && Ss.Contains((R + 1, C - 1)) && Ms.Contains((R + 1, C + 1)) && Ss.Contains((R - 1, C - 1)))
            {
                n++;
            }
        }
        return n;
    }

    private static void LoadData(string fileName)
    {
        Xs = [];
        Ms = [];
        As = [];
        Ss = [];
        var lines = new DataLoader(2024, 4).ReadStrings(fileName).ToList();
        for(var row = 0; row < lines.Count; row++)
        {
            for(var col = 0; col < lines.First().Length; col++)
            {
                if (lines[row][col] == 'X')
                {
                    Xs.Add((row, col));
                }
                if (lines[row][col] == 'M')
                {
                    Ms.Add((row, col));
                }
                if (lines[row][col] == 'A')
                {
                    As.Add((row, col));
                }
                if (lines[row][col] == 'S')
                {
                    Ss.Add((row, col));
                }
            }
        }
    }

    private static HashSet<(int R, int C)> Xs = [];
    private static HashSet<(int R, int C)> Ms = [];
    private static HashSet<(int R, int C)> As = [];
    private static HashSet<(int R, int C)> Ss = [];
}
