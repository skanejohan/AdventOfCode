using CSharpLib;
using System.Linq;
using System.Collections.Generic;
using CSharpLib.Algorithms;
using System;

namespace Y2024.Day18;

public static class Solver
{
    public static long Part1()
    {
        var maxX = 70;
        var maxY = 70;
        var noOfBytes = 1024;
        var memory = new HashSet<(int, int)>();
        Load("data.txt", noOfBytes, pos => { memory.Add(pos); });
        return Solve(maxX, maxY, memory);
    }

    public static string Part2()
    {
        var maxX = 70;
        var maxY = 70;
        var noOfBytes = 1024;
        var fileName = "data.txt";
        var memory = new HashSet<(int, int)>();
        while (true)
        {
            try
            {
                Load(fileName, noOfBytes, pos => { memory.Add(pos); });
                Solve(maxX, maxY, memory);
                noOfBytes++;
            }
            catch 
            {
                var list = new List<(int, int)>();
                Load(fileName, noOfBytes, list.Add);
                return $"{list.Last()}".Replace("(", "").Replace(")", "").Replace(" ", "");
            }
        }
    }

    private static long Solve(int maxX, int maxY, HashSet<(int X, int Y)> memory)
    {
        return Dijkstra<(int, int)>.Solve((0, 0), getNeighbors, p => p == (maxX, maxY)).TotalCost;

        IEnumerable<((int X, int Y), long)> getNeighbors((int X, int Y) pos)
        {
            List<(int X, int Y)> neighbors = [(pos.X - 1, pos.Y), (pos.X, pos.Y - 1), (pos.X + 1, pos.Y), (pos.X, pos.Y + 1)];
            foreach (var p in neighbors)
            {
                if (p.X >= 0 && p.X <= maxX && p.Y >= 0 && p.Y <= maxY && !memory.Contains(p))
                {
                    yield return (p, 1L);
                }
            }
        }
    }

    private static void Load(string fileName, int count, Action<(int X, int Y)> action)
    {
        var n = 0;
        foreach (var pos in new DataLoader("2024", 18).ReadStrings(fileName).Select(s => s.Split(',').Select(int.Parse)))
        {
            action((pos.First(), pos.Skip(1).First()));
            n++;
            if (n == count)
            {
                break;
            }
        }
    }

    //public static HashSet<(int X, int Y)> LoadMemorySet(string fileName, int count)
    //{
    //    var n = 0;
    //    var memorySet = new HashSet<(int, int)>();
    //    foreach (var pos in new DataLoader("2024", 18).ReadStrings(fileName).Select(s => s.Split(',').Select(int.Parse)))
    //    {
    //        memorySet.Add((pos.First(), pos.Skip(1).First()));
    //        memoryList.Add((pos.First(), pos.Skip(1).First()));
    //        //n++;
    //        //if (n == count)
    //        //{
    //        //    break;
    //        //}
    //    }
    //    return (memorySet, memoryList);
    //}

    //public static (HashSet<(int X, int Y)> Set, List<(int X, int Y)>) LoadData(string fileName, int count)
    //{
    //    var n = 0;
    //    var memorySet = new HashSet<(int, int)>();
    //    var memoryList = new List<(int, int)>();
    //    foreach (var pos in new DataLoader("2024", 18).ReadStrings(fileName).Select(s => s.Split(',').Select(int.Parse)))
    //    {
    //        memorySet.Add((pos.First(), pos.Skip(1).First()));
    //        memoryList.Add((pos.First(), pos.Skip(1).First()));
    //        //n++;
    //        //if (n == count)
    //        //{
    //        //    break;
    //        //}
    //    }
    //    return (memorySet, memoryList);
    //}
}
