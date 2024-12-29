using CSharpLib.DataStructures;
using CSharpLib;
using System.Collections.Generic;
using System.Linq;
using System;
using CSharpLib.Utils;

namespace Y2024.Day20;

public static class Solver
{
    public static long Part1()
    {
        var distances = LoadData("data.txt");
        return HowManyCheatsAreNeeded(distances, 100, 2);
    }

    public static long Part2()
    {
        var distances = LoadData("data.txt");
        return HowManyCheatsAreNeeded(distances, 100, 20);
    }

    static int HowManyCheatsAreNeeded(Dictionary<(int, int), long> cellsWithDistanceToGoal, int cheatLevel, int cheatLength)
    {
        var n = 0;
        foreach (var cell in cellsWithDistanceToGoal)
        {
            n += cellsWithDistanceToGoal
                .Where(c => c.Key != cell.Key && GeometryUtils.ManhattanDistance(cell.Key, c.Key) <= cheatLength)
                .Where(c => cell.Value - c.Value - GeometryUtils.ManhattanDistance(cell.Key, c.Key) >= cheatLevel)
                .Count();
        }
        return n;
    }

    public static Dictionary<(int, int), long> LoadData(string fileName)
    {
        var startPos = (0, 0);
        var endPos = (0, 0);
        var distances = new Dictionary<(int, int), long>();
        var grid = new Grid<char>(new DataLoader("2024", 20).ReadStrings(fileName));
        foreach (var (Row, Col, Value) in grid)
        {
            switch (Value)
            {
                case 'S':
                    startPos = (Row, Col);
                    break;
                case 'E':
                    endPos = (Row, Col);
                    break;
                default:
                    break;
            }
        }
        var pos = endPos;
        HashSet<(int, int)> seen = [];
        distances[pos] = 0;
        seen.Add(pos);
        while (pos != startPos)
        {
            var d = distances[pos];
            var (Row, Col, Value) = grid.GetNeighbors4(pos).Where(n => !seen.Contains((n.Row, n.Col)) && grid.Get(n.Row, n.Col) != '#').First();
            pos = (Row, Col);
            distances[pos] = d + 1;
            seen.Add(pos);
        }
        return distances;
    }
}