using CSharpLib;
using CSharpLib.Algorithms;
using CSharpLib.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;

namespace Y2024.Day16;

public static class Solver
{
    public static long Part1()
    {
        var (startPos, endPos) = LoadData("Data.txt");
        var start = (startPos, (0, 1), );
        return Dijkstra<((int Row, int Col), (int Row, int Col))>.Solve(start, GetNeighbors, pos => pos.Item1 == endPos).TotalCost;
    }

    record Pos(int Row, int Col);
    record Dir(int Row, int Col);
    record State(Pos Pos, Dir Dir, List<(Pos, Dir)> Path);

    public static long Part2()
    {
        var (startPos, endPos) = LoadData("TestData.txt");
        var start = (startPos, (0, 1));

        Dijkstra<((int Row, int Col), (int Row, int Col), List<((int Row, int Col), (int Row, int Col))>)>.Solve(



        var n = 0;
        var targetCost = Dijkstra<((int Row, int Col), (int Row, int Col))>.SolveEx(start, GetNeighbors, End).TotalCost;

        return targetCost;

        bool End(((int, int), (int, int)) pos, List<(((int, int), (int, int)), long)> path, long cost)
        {
            if (pos.Item1 != endPos)
            {
                return false;
            }
            n++;
            return n == 2 && cost == 7036;
        }

        //var targetCost = Dijkstra<((int Row, int Col), (int Row, int Col))>.Solve(start, getNeighbors, pos => pos.Item1 == endPos).TotalCost;
        //List<List<(((int, int), (int, int)), long)>> solutions = [];
        //HashSet<(int, int)> visitedPositions = [];
        //var allFound = false;
        //while(!allFound)
        //{
        //    try 
        //    {
        //        var solution = Dijkstra<((int Row, int Col), (int Row, int Col))>
        //            .SolveEx(start, getNeighbors, (pos, path, cost) => pos.Item1 == endPos && cost == targetCost && !solutionAlreadyFound(path));
        //        solutions.Add(solution.Path);
        //        var positionsInThisSolution = solution.Path.Select(n => n.Item1.Item1);
        //        visitedPositions.UnionWith(positionsInThisSolution);
        //    }
        //    catch
        //    {
        //        allFound = true;
        //    }
        //}
        //return visitedPositions.Count;

        //bool solutionAlreadyFound(List<(((int, int), (int, int)), long)> solution)
        //{
        //    return solutions.Any(s => s.SequenceEqual(solution));
        //}
    }

    private static IEnumerable<(((int, int), (int, int)), long)> GetNeighbors(((int Row, int Col) Pos, (int Row, int Col) Dir) state)
    {
        yield return ((state.Pos, Clockwise(state.Dir)), 1000L);
        yield return ((state.Pos, CounterClockwise(state.Dir)), 1000L);
        var targetPos = (state.Pos.Row + state.Dir.Row, state.Pos.Col + state.Dir.Col);
        if (!Walls.Contains(targetPos))
        {
            yield return ((targetPos, state.Dir), 1L);
        }
    }

    static (int, int) Clockwise((int, int) dir)
    {
        return dir switch
        {
            (0, 1) => (1, 0),
            (1, 0) => (0, -1),
            (0, -1) => (-1, 0),
            _ => (0, 1)
        };
    }

    static (int, int) CounterClockwise((int, int) dir)
    {
        return dir switch
        {
            (0, 1) => (-1, 0),
            (-1, 0) => (0, -1),
            (0, -1) => (1, 0),
            _ => (0, 1)
        };
    }

    public static ((int Row, int Col) Start, (int Row, int Col) End) LoadData(string fileName)
    {
        Walls = [];
        (int Row, int Col) start = (0, 0);
        (int Row, int Col) end = (0, 0);
        foreach (var (Row, Col, Value) in new Grid<char>(new DataLoader("2024", 16).ReadStrings(fileName)))
        {
            switch (Value)
            {
                case '#':
                    Walls.Add((Row, Col));
                    break;
                case 'S':
                    start = (Row, Col);
                    break;
                case 'E':
                    end = (Row, Col);
                    break;
                default:
                    break;
            }
        }
        return (start, end);
    }

    static HashSet<(int Row, int Col)> Walls = [];
}
