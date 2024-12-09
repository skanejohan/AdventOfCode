using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day06;

public static class Solver
{
    public static long Part1()
    {
        LoadData("Data.txt");
        path = [];
        pos = initialPos;
        dir = (-1, 0);
        Calculate();
        return UniqueVisitedPositions().Count;
    }

    public static long Part2()
    {
        Part1();
        var previousPath = new List<((int Row, int Col) Pos, (int DRow, int DCol) Dir)>(path);
        var extraObstacles = UniqueVisitedPositions();

        var environmentsWithoutLoop = 0;
        foreach (var o in extraObstacles)
        {
            path = [];
            extraObstacle = o;
            for (int i = previousPath.Count - 1; i > 0; i--)
            {
                if (previousPath[i].Pos == extraObstacle)
                {
                    pos = previousPath[i - 1].Pos;
                    dir = previousPath[i - 1].Dir;
                }
            }
            if (Calculate())
            {
                environmentsWithoutLoop++;
            }
        }
        return environmentsWithoutLoop;
    }

    static bool Calculate()
    {
        var visited = new HashSet<((int Row, int Col), (int DRow, int DCol))>();

        var loop = false;
        while (InMap())
        {
            if (visited.Contains((pos, dir)))
            {
                loop = true;
                break;
            }

            visited.Add((pos, dir));

            if (!Move())
            {
                Turn();
            }

            path.Add((pos, dir));
        }
        return loop;
    }

    static HashSet<(int Row, int Col)> UniqueVisitedPositions()
    {
        return path.Select(x => x.Pos).ToHashSet();
    }

    static bool Move()
    {
        var newPos = (pos.Row + dir.DRow, pos.Col + dir.DCol);
        if (obstacles.Contains(newPos) || extraObstacle == newPos)
        {
            return false;
        }
        pos = newPos;
        return true;
    }

    static void Turn()
    {
        dir = dir switch
        {
            (-1, +0) => (+0, +1),
            (+0, +1) => (+1, +0),
            (+1, +0) => (+0, -1),
            (+0, -1) => (-1, +0),
            _ => throw new Exception()
        };
    }

    static bool InMap()
    {
        return pos.Row >= 0 && pos.Row <= maxRow && pos.Col >= 0 && pos.Col <= maxCol;
    }

    static void LoadData(string fileName)
    {
        var rows = new DataLoader(2024, 6).ReadEnumerableChars(fileName);
        obstacles = [];

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
                    initialPos = (r, c);
                }
                c++;
            }
            r++;
        }

        maxRow = rows.Count() - 1;
        maxCol = rows.First().Count() - 1;
    }

    // Will change during calculation
    static (int Row, int Col) pos;
    static (int DRow, int DCol) dir;
    static (int Row, int Col)? extraObstacle;
    static List<((int Row, int Col) Pos, (int DRow, int DCol) Dir)> path = [];

    // Set by LoadData and then not changed
    static HashSet<(int Row, int Col)> obstacles = [];
    static (int Row, int Col) initialPos;
    static int maxRow;
    static int maxCol;
}
