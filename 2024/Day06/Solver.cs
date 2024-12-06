using CSharpLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;

namespace Y2024.Day06;

public static class Solver
{
    public static long Part1()
    {
        var state = CalcStateForPartOne("Data.txt");
        return state.Visited.Count - 1;
    }

    public static long Part2()
    {
        var interestingPositions = CalcStateForPartOne("Data.txt").Visited;

        var (env, state) = LoadData("Data.txt");
        var n = 0;
        var m = 0;
        foreach(var extraObstacle in interestingPositions)
        {
            m++;
            var s = new State(state.Pos, state.Dir, state.Visited);
            var e = new Env(new HashSet<(int Row, int Col)>(env.Obstacles) { extraObstacle }, env.MaxRow, env.MaxCol);

            var moves = 0;
            var maxMoves = e.MaxCol * e.MaxRow;
            while (InMap(e, s) && moves < maxMoves)
            {
                if (Move(e, s, out var newState))
                {
                    s = newState;
                    moves++;
                }
                else
                {
                    s = Turn(s);
                }
            }
            if (InMap(e, s))
            {
                n++;
            }
        }
        return n;
    }

    static State CalcStateForPartOne(string fileName)
    {
        var (env, state) = LoadData(fileName);
        while (InMap(env, state))
        {
            if (Move(env, state, out var newState))
            {
                state = newState;
            }
            else
            {
                state = Turn(state);
            }
        }
        return state;
    }

    static bool Move(Env env, State state, out State newState)
    {
        var newPos = (state.Pos.Row + state.Dir.DRow, state.Pos.Col + state.Dir.DCol);
        if (env.Obstacles.Contains(newPos))
        {
            newState = state;
            return false;
        }
        newState = state with { Pos = newPos, Visited = new HashSet<(int Row, int Col)>(state.Visited) { newPos } };
        return true;
    }

    static State Turn(State state)
    {
        return state.Dir switch
        {
            (-1, +0) => state with { Dir = (+0, +1) },
            (+0, +1) => state with { Dir = (+1, +0) },
            (+1, +0) => state with { Dir = (+0, -1) },
            (+0, -1) => state with { Dir = (-1, +0) },
            _ => throw new System.Exception()
        };
    }

    static bool InMap(Env env, State state)
    {
        return state.Pos.Row >= 0 && state.Pos.Row <= env.MaxRow && state.Pos.Col >= 0 && state.Pos.Col <= env.MaxCol;
    }

    static (Env, State) LoadData(string fileName)
    {
        (int Row, int Col) pos = (0, 0);
        HashSet<(int Row, int Col)> obstacles = [];

        var rows = new DataLoader(2024, 6).ReadEnumerableChars(fileName);

        var r = 0;
        foreach(var row in rows)
        {
            var c = 0;
            foreach(var col in row)
            {
                if (col == '#')
                {
                    obstacles.Add((r, c));
                }
                if (col == '^')
                {
                    pos = (r, c);
                }
                c++;
            }
            r++;
        }

        return (new Env(obstacles, rows.Count() - 1, rows.First().Count() - 1), new State(pos, (-1, 0), [pos]));
    }

    record Env(HashSet<(int Row, int Col)> Obstacles, int MaxRow, int MaxCol);
    record State((int Row, int Col) Pos, (int DRow, int DCol) Dir, HashSet<(int Row, int Col)> Visited);
}
