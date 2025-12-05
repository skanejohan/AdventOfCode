using CSharpLib;
using CSharpLib.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day04;

public static class Solver
{
    public static long Part1()
    {
        return Accessible(LoadData("Data.txt")).Count();
    }

    public static long Part2()
    {
        var count = 0;
        var grid = LoadData("Data.txt");
        while(true)
        {
            var accessible = Accessible(grid).ToList();
            foreach(var (Row, Col, Value) in accessible)
            {
                grid.Set(Row, Col, '.');
            }
            count += accessible.Count;
            if (accessible.Count == 0)
            { 
                break; 
            }
        }
        return count;
    }

    private static Grid<char> LoadData(string fileName)
    {
        return new Grid<char>(new DataLoader(2025, 4).ReadEnumerableChars(fileName));
    }

    private static IEnumerable<(int Row, int Col, char Value)> Accessible(Grid<char> grid)
    {
        foreach (var cell in grid.Where(c => c.Value == '@'))
        {
            var neighbors = grid.GetNeighbors8(cell.Row, cell.Col);
            if (neighbors.Count(n => n.Value == '@') < 4)
            {
                yield return cell;
            }
        }
    }
}
