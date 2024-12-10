using CSharpLib.DataStructures;
using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day10;

public static class Solver
{
    public static long Part1()
    {
        var n = 0;
        var map = new Grid<int>(new DataLoader("2024", 10).ReadEnumerableInts("Data.txt"));

        var reachable = new Dictionary<(int Row, int Col), HashSet<(int Row, int Col)>>();

        foreach (var cell in map.Where(c => c.Value == 9))
        {
            reachable[(cell.Row, cell.Col)] = new HashSet<(int Row, int Col)> { (cell.Row, cell.Col) };
        }

        for (var i = 8; i >= 0; i--)
        {
            foreach (var cell in map.Where(c => c.Value == i))
            {
                var neighbors = map.GetNeighbors4((cell.Row, cell.Col)).Where(n => n.Value == cell.Value + 1);
                reachable[(cell.Row, cell.Col)] = new HashSet<(int Row, int Col)>();
                foreach(var neighbor in neighbors)
                {
                    reachable[(cell.Row, cell.Col)].UnionWith(reachable[(neighbor.Row, neighbor.Col)]);
                }
            }
        }

        foreach (var cell in map.Where(c => c.Value == 0))
        {
            n += reachable[(cell.Row, cell.Col)].Count;
        }
        return n;
    }

    public static long Part2()
    {
        var n = 0;
        var map = new Grid<int>(new DataLoader("2024", 10).ReadEnumerableInts("Data.txt"));
        var reachable = new Dictionary<(int Row, int Col), int>();

        foreach (var cell in map.Where(c => c.Value == 9))
        {
            reachable[(cell.Row, cell.Col)] = 1;
        }

        for (var i = 8; i >= 0; i--)
        {
            foreach (var cell in map.Where(c => c.Value == i))
            {
                var v = map.GetNeighbors4((cell.Row, cell.Col)).Where(n => n.Value == cell.Value + 1).Select(n => reachable[(n.Row, n.Col)]).Sum(); ;
                reachable[(cell.Row, cell.Col)] = v;
            }
        }

        foreach (var cell in map.Where(c => c.Value == 0))
        {
            n += reachable[(cell.Row, cell.Col)];
        }

        return n;
    }
}
