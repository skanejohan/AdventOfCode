using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day09;

public static class Solver
{
    public static long Part1()
    {
        var redTiles = LoadRedTiles("Data.txt");
        var rectangles = GetRectangles(redTiles);

        long area = 0;
        foreach(var (X1, Y1, X2, Y2, Area) in rectangles)
        {
            area = Math.Max(area, Area);
        }
        return area;
    }

    public static long Part2() // 4743386600 is too high
    {
        var redTiles = LoadRedTiles("Data.txt");
        var (horizontalEdges, verticalEdges) = GetEdges(redTiles);
        var rectangles = GetRectangles(redTiles).Where(r => InsidePolygon((r.X1, r.Y1, r.X2, r.Y2), horizontalEdges, verticalEdges));

        long area = 0;
        foreach (var (X1, Y1, X2, Y2, Area) in rectangles)
        {
            area = Math.Max(area, Area);
        }
        //return area;
        return 0;
    }

    private static List<(int X, int Y)> LoadRedTiles(string fileName)
    {
        return new DataLoader(2025, 9)
            .ReadStrings(fileName)
            .Select(s => s.Split(','))
            .Select(s => { var l = s.Select(int.Parse).ToList(); return (l[0], l[1]); })
            .ToList();
    }

    private static IEnumerable<(int X1, int Y1, int X2, int Y2, long Area)> GetRectangles(List<(int X, int Y)> redTiles)
    {
        for (var i = 0; i < redTiles.Count - 1; i++)
        {
            for (var j = i + 1; j < redTiles.Count; j++)
            {
                var (X1, Y1) = redTiles[i];
                var (X2, Y2) = redTiles[j];
                long dx = Math.Abs(X2 - X1) + 1;
                long dy = Math.Abs(Y2 - Y1) + 1;
                yield return (X1, Y1, X2, Y2, dx * dy);
            }
        }
    }

    private static (List<(int X1, int X2, int Y)> Horizontal, List<(int X, int Y1, int Y2)> Vertical) GetEdges(List<(int X, int Y)> redTiles)
    {
        var allPoints = redTiles.Append(redTiles[0]).ToList();
        var edges = new List<(int X1, int Y1, int X2, int Y2)>();
        for (var i = 0; i < allPoints.Count - 1; i++)
        {
            var p1 = allPoints[i];
            var p2 = allPoints[i + 1];
            edges.Add((p1.X, p1.Y, p2.X, p2.Y));
        }
        var horizontal = edges.Where(e => e.Y1 == e.Y2).Select(e => e.X1 < e.X2 ? (e.X1, e.X2, e.Y1) : (e.X2, e.X1, e.Y1)).ToList();
        var vertical = edges.Where(e => e.X1 == e.X2).Select(e => e.Y1 < e.Y2 ? (e.X1, e.Y1, e.Y2) : (e.X1, e.Y2, e.Y1)).ToList();
        return (horizontal, vertical);
    }

    private static bool InsidePolygon((int X1, int Y1, int X2, int Y2) rect, List<(int X1, int X2, int Y)> horizontalEdges, List<(int X, int Y1, int Y2)> verticalEdges)
    {
        var topLeftInside = InsidePolygon((rect.X1, rect.Y1), horizontalEdges, verticalEdges);
        var topRightInside = InsidePolygon((rect.X2, rect.Y1), horizontalEdges, verticalEdges);
        var bottomRightInside = InsidePolygon((rect.X2, rect.Y2), horizontalEdges, verticalEdges);
        var bottomLeftInside = InsidePolygon((rect.X1, rect.Y2), horizontalEdges, verticalEdges);

        return topLeftInside && topRightInside && bottomRightInside && bottomLeftInside;
    }

    private static bool InsidePolygon((int X, int Y) pos, List<(int X1, int X2, int Y)> horizontalEdges, List<(int X, int Y1, int Y2)> verticalEdges)
    {
        // Does the point lie on a horizontal line?
        foreach (var (X1, X2, Y) in horizontalEdges.Where(e => e.Y == pos.Y))
        {
            if (X1 <= pos.X && X2 >= pos.X)
            {
                return true;
            }
        }

        // How many vertical lines (that will get crossed if moving horizontally) are further to the right than the point? Odd => Inside, even => not inside.
        var crossableVerticalEdges = verticalEdges.Where(e => e.Y1 <= pos.Y && e.Y2 >= pos.Y);
        var crossedVerticalEdges = crossableVerticalEdges.Where(e => e.X > pos.X);
        return crossedVerticalEdges.Count() % 2 != 0;
    }
}
