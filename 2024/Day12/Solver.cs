using CSharpLib.DataStructures;
using CSharpLib;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Y2024.Day12;

public static class Solver
{
    public static long Part1()
    {
        return Solve("Data.txt", PriceOfRegion);

        static long PriceOfRegion(HashSet<(int R, int C)> region)
        {
            var l = 0;
            var a = 0;
            foreach(var (R, C) in region)
            {
                if (!region.Contains(((R - 1), C)))
                {
                    l++; // Upper fence
                }
                if (!region.Contains(((R + 1), C)))
                {
                    l++; // Lower fence
                }
                if (!region.Contains((R, (C - 1))))
                {
                    l++; // Left fence
                }
                if (!region.Contains((R, (C + 1))))
                {
                    l++; // Right fence
                }
                a++; // Area
            }
            var p = l * a;
            return p;
        }
    }

    public static long Part2()
    {
        return Solve("Data.txt", PriceOfRegion);

        static long PriceOfRegion(HashSet<(int R, int C)> region)
        {
            var cellsWithLeftEdge = region.Where(c => !region.Contains((c.R, c.C - 1))).ToHashSet();
            var topCellsWithLeftEdge = cellsWithLeftEdge.Where(c => !cellsWithLeftEdge.Contains((c.R - 1, c.C)));
            var cellsWithTopEdge = region.Where(c => !region.Contains((c.R - 1, c.C))).ToHashSet();
            var leftCellsWithTopEdge = cellsWithTopEdge.Where(c => !cellsWithTopEdge.Contains((c.R, c.C - 1)));
            var cellsWithRightEdge = region.Where(c => !region.Contains((c.R, c.C + 1))).ToHashSet();
            var topCellsWithRightEdge = cellsWithRightEdge.Where(c => !cellsWithRightEdge.Contains((c.R - 1, c.C)));
            var cellsWithBottomEdge = region.Where(c => !region.Contains((c.R + 1, c.C))).ToHashSet();
            var leftCellsWithBottomEdge = cellsWithBottomEdge.Where(c => !cellsWithBottomEdge.Contains((c.R, c.C - 1)));
            return (topCellsWithLeftEdge.Count() + leftCellsWithTopEdge.Count() + topCellsWithRightEdge.Count() + leftCellsWithBottomEdge.Count()) * region.Count();
        }
    }

    static long Solve(string fileName, Func<HashSet<(int R, int C)>, long> regionPriceCalculator)
    {
        var price = 0L;
        visited.Clear();
        map = new Grid<char>(new DataLoader("2024", 12).ReadEnumerableChars(fileName));
        foreach (var cell in map)
        {
            var region = CalculateRegion(cell);
            price += regionPriceCalculator(region);
        }
        return price;
    }

    static HashSet<(int R, int C)> CalculateRegion((int Row, int Col, int Value) cell) // Also adds them all to visited
    {
        var currentRegion = new HashSet<(int R, int C)>();
        Calculate(cell);
        return currentRegion;

        void Calculate((int Row, int Col, int Value) cell)
        {
            var pos = (cell.Row, cell.Col);
            if (!visited.Contains(pos))
            {
                currentRegion.Add(pos);
                visited.Add(pos);
                foreach (var n in map.GetNeighbors4(pos))
                {
                    if (n.Value == cell.Value)
                    {
                        Calculate(n);
                    }
                }
            }
        }
    }

    static HashSet<(int R, int C)> visited = [];
    static Grid<char> map;
}
