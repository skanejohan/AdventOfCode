using System;
using System.Collections.Generic;

namespace Y2024.Day21;

public static class Solver
{
    public static long Part1()
    {
        return Solve(3, "463A") + Solve(3, "340A") + Solve(3, "129A") + Solve(3, "083A") + Solve(3, "341A");

    }

    public static long Part2()
    {
        return Solve(26, "463A") + Solve(26, "340A") + Solve(26, "129A") + Solve(26, "083A") + Solve(26, "341A");
    }

    private static long Solve(int noOfRobots, string target)
    {
        var result = 0L;
        var currentNumPadPosition = numPadPositions['A'];
        
        foreach (var c in target)
        {
            var targetNumPadPosition = numPadPositions[c];
            result += CheapestToNumPadPosition(currentNumPadPosition, targetNumPadPosition);
            currentNumPadPosition = targetNumPadPosition;
        }

        return long.Parse(target.Replace("A", "")) * result;

        long CheapestToNumPadPosition((int Row, int Col) current, (int Row, int Col) target)
        {
            var result = long.MaxValue;
            var queue = new Queue<State>();
            queue.Enqueue(new State(current, ""));
            while(queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (state.Pos == target)
                {
                    result = Math.Min(result, Robot(state.KeysPressed + "A", noOfRobots));
                }
                else 
                {
                    EnqueueMoves(state.Pos, target, (3, 0), state.KeysPressed, queue);
                }
            }
            return result;
        }

        long CheapestToDirPadPosition((int Row, int Col) current, (int Row, int Col) target, int noOfRobots)
        {
            if (memo.TryGetValue((current, target, noOfRobots), out var result))
            {
                return result;
            }

            result = long.MaxValue;
            var queue = new Queue<State>();
            queue.Enqueue(new State(current, ""));
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (state.Pos == target)
                {
                    result = Math.Min(result, Robot(state.KeysPressed + "A", noOfRobots - 1));
                }
                else
                {
                    EnqueueMoves(state.Pos, target, (0, 0), state.KeysPressed, queue);
                }
            }
            memo[(current, target, noOfRobots)] = result;
            return result;
        }

        long Robot(string keysPressed, int noOfRobots)
        {
            if (noOfRobots == 1)
            {
                return keysPressed.Length;
            }

            var result = 0L;
            var current = dirPadPositions['A'];
            foreach (var key in keysPressed)
            {
                var target = dirPadPositions[key];
                result += CheapestToDirPadPosition(current, target, noOfRobots);
                current = target;
            }
            return result;
        }

        void EnqueueMoves((int Row, int Col) current, (int Row, int Col) target, (int Row, int Col) excluded, string keysPressed, Queue<State> queue)
        {
            int row = current.Row;
            int col = current.Col;
            if (row < target.Row)
            {
                Enqueue((row + 1, col), 'v');
            }
            if (row > target.Row)
            {
                Enqueue((row - 1, col), '^');
            }
            if (col < target.Col)
            {
                Enqueue((row, col + 1), '>');
            }
            if (col > target.Col)
            {
                Enqueue((row, col - 1), '<');
            }

            void Enqueue((int Row, int Col) pos, char c)
            {
                if (pos != excluded)
                {
                    queue.Enqueue(new State(pos, keysPressed + c));
                }
            }
        }
    }

    record State((int Row, int Col) Pos, string KeysPressed);

    static readonly Dictionary<char, (int Row, int Col)> numPadPositions = new()
    {
        { '7', (0, 0) },
        { '8', (0, 1) },
        { '9', (0, 2) },
        { '4', (1, 0) },
        { '5', (1, 1) },
        { '6', (1, 2) },
        { '1', (2, 0) },
        { '2', (2, 1) },
        { '3', (2, 2) },
        { '0', (3, 1) },
        { 'A', (3, 2) }
    };

    static readonly Dictionary<char, (int Row, int Col)> dirPadPositions = new()
    {
        { '^', (0, 1) },
        { 'A', (0, 2) },
        { '<', (1, 0) },
        { 'v', (1, 1) },
        { '>', (1, 2) }
    };

    static readonly Dictionary<((int Row, int Col) Current, (int Row, int Col) Target, int NoOfRobots), long> memo = [];
}
