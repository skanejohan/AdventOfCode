using CSharpLib;
using CSharpLib.Algorithms;
using CSharpLib.DataStructures;
using System.Collections.Generic;

namespace Y2024.Day16;

public static class Solver
{
    public static long Part1()
    {
        LoadData("Data.txt");
        return SolveFrom((StartPos, (0, 1))).TotalCost;
    }

    public static long Part2()
    {
        LoadData("Data.txt");

        var solution = SolveFrom((StartPos, (0, 1))).TotalCost;

        // Idea: for each cell in solution, calculate its cost and add to a dictionary from state to cost.
        // This will by definition be the lowest cost for the state in question.
        // Place the solution path on a stack.
        // While the stack is not empty
        //   Pop the top path.
        //   For each of its nodes
        //     Create a new state, rotated clockwise
        //     If the new state is not in the cost dictionary 
        //       Calculate the shortest path from the start node to this state, just to get the cost
        //       Add it to the cost dictionary
        //       Calculate the shortest path from the state to the end state
        //       If a solution exists, update the cost dictionary and push the solution to the stack
        //     Create a new state, rotated counterclockwise
        //     repeat as above

        return 0L;
    }

    static Solution<((int Row, int Col) Pos, (int Row, int Col) Dir)> SolveFrom(((int Row, int Col) Pos, (int Row, int Col) Dir) start)
    {
        return Dijkstra<((int Row, int Col), (int Row, int Col))>.Solve(start, GetNeighbors, pos => pos.Item1 == EndPos);

        static IEnumerable<(((int, int), (int, int)), long)> GetNeighbors(((int Row, int Col) Pos, (int Row, int Col) Dir) state)
        {
            yield return ((state.Pos, Clockwise[state.Dir]), 1000L);
            yield return ((state.Pos, CounterClockwise[state.Dir]), 1000L);
            var targetPos = (state.Pos.Row + state.Dir.Row, state.Pos.Col + state.Dir.Col);
            if (!Walls.Contains(targetPos))
            {
                yield return ((targetPos, state.Dir), 1L);
            }
        }
    }

    public static void LoadData(string fileName)
    {
        Walls = [];
        foreach (var (Row, Col, Value) in new Grid<char>(new DataLoader("2024", 16).ReadStrings(fileName)))
        {
            switch (Value)
            {
                case '#':
                    Walls.Add((Row, Col));
                    break;
                case 'S':
                    StartPos = (Row, Col);
                    break;
                case 'E':
                    EndPos = (Row, Col);
                    break;
                default:
                    break;
            }
        }
    }

    static readonly Dictionary<(int, int), (int, int)> Clockwise = new() { { (0, 1), (1, 0) }, { (1, 0), (0, -1) }, { (0, -1), (-1, 0) }, { (-1, 0) , (0, 1) } };
    static readonly Dictionary<(int, int), (int, int)> CounterClockwise = new() { { (0, 1), (-1, 0) }, { (-1, 0), (0, -1) }, { (0, -1), (1, 0) }, { (1, 0), (0, 1) } };

    static HashSet<(int Row, int Col)> Walls = [];
    static (int Row, int Col) StartPos = (0, 0);
    static (int Row, int Col) EndPos = (0, 0);
}
