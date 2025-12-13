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
        return rectangles.Select(r => r.Area()).Max();
    }

    public static long Part2() // 4743386600 is too high, 4638696212 is too high, 2125643728 is too high, 2019428457 is wrong
    {
        var redTiles = LoadRedTiles("Data.txt");
        var rectangles = GetRectangles(redTiles);
        var (horizontal, vertical) = GetEdges([.. redTiles, redTiles[0]]);
        return rectangles.Where(r => IsValidRectangle(r, horizontal, vertical)).Select(r => r.Area()).Max();
    }

    private static List<Point> LoadRedTiles(string fileName)
    {
        return [.. new DataLoader(2025, 9).ReadStrings(fileName).Select(s => s.Split(',')).Select(s => { var l = s.Select(int.Parse).ToList(); return new Point(l[0], l[1]); })];
    }

    private static IEnumerable<Rectangle> GetRectangles(List<Point> redTiles)
    {
        for (var i = 0; i < redTiles.Count - 1; i++)
        {
            for (var j = i + 1; j < redTiles.Count; j++)
            {
                yield return new Rectangle(redTiles[i].X, redTiles[i].Y, redTiles[j].X, redTiles[j].Y);
            }
        }
    }

    private static (List<HLine> Horizontal, List<VLine> Vertical) GetEdges(List<Point> polygon)
    {
        List<VLine> vertical = [];
        List<HLine> horizontal = [];
        for (var i = 0; i < polygon.Count - 1; i++)
        {
            var p1 = polygon[i];
            var p2 = polygon[i + 1];
            if (p1.X == p2.X)
            {
                vertical.Add(new VLine(p1.X, p1.Y, p2.Y));
            }
            else
            {
                horizontal.Add(new HLine(p1.X, p2.X, p1.Y));
            }
        }
        return (horizontal, vertical);
    }

    private static bool IsValidRectangle(Rectangle r, List<HLine> horizontalEdges, List<VLine> verticalEdges)
    {
        // Check that all four corners are inside the polygon, or return false.
        foreach(var p in r.Corners())
        {
            if (!IsInPolygon(p, horizontalEdges, verticalEdges))
            {
                return false;
            }
        }

        // If the rectangle's left or right edge crosses any horizontal edge in the polygon, return false.
        foreach (var hEdge in horizontalEdges)
        {
            foreach(var vEdge in r.VEdges())
            {
                if (LinesCross(vEdge, hEdge))
                {
                    return false;
                }
            }
        }

        // If the rectangle's top or bottom edge crosses any vertical edge in the polygon, return false.
        foreach (var vEdge in verticalEdges)
        {
            foreach (var hEdge in r.HEdges())
            {
                if (LinesCross(vEdge, hEdge))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsInPolygon(Point p, List<HLine> horizontalEdges, List<VLine> verticalEdges)
    {
        if (horizontalEdges.Any(e => e.ContainsPoint(p)))
        {
            return true;
        }

        if (verticalEdges.Any(e => e.ContainsPoint(p)))
        {
            return true;
        }

        // How many vertical edges would we cross if we move to the right from this point? It if is an odd number, we are inside the polygon.
        var edgesToTheRight = verticalEdges.Where(e => e.X > p.X && e.MinY <= p.Y && e.MaxY >= p.Y).Count();
        return edgesToTheRight % 2 == 1;
    }

    private static bool LinesCross(VLine vLine, HLine hLine)
    {
        return hLine.MinX < vLine.X && hLine.MaxX > vLine.X && vLine.MinY < hLine.Y && vLine.MaxY > hLine.Y;
    }

    class Point(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }

    class Rectangle(int x1, int y1, int x2, int y2)
    {
        public int X1 { get; } = x1;
        public int Y1 { get; } = y1;
        public int X2 { get; } = x2;
        public int Y2 { get; } = y2;
        public long Area()
        {
            long dx = Math.Abs(X2 - X1) + 1;
            long dy = Math.Abs(Y2 - Y1) + 1;
            return dx * dy;
        }
        public IEnumerable<Point> Corners()
        {
            yield return new Point(X1, Y1);
            yield return new Point(X2, Y1);
            yield return new Point(X2, Y2);
            yield return new Point(X1, Y2);
        }
        public IEnumerable<HLine> HEdges()
        {
            yield return new HLine(X1, X2, Y1);
            yield return new HLine(X1, X2, Y2);
        }
        public IEnumerable<VLine> VEdges()
        {
            yield return new VLine(X1, Y1, Y2);
            yield return new VLine(X2, Y1, Y2);
        }
    }

    class HLine(int x1, int x2, int y)
    {
        public int X1 { get; } = x1;
        public int X2 { get; } = x2;
        public int Y { get; } = y;
        public int MinX => Math.Min(X1, X2);
        public int MaxX => Math.Max(X1, X2);
        public bool ContainsPoint(Point p) => p.Y == Y && p.X >= MinX && p.X <= MaxX;
    }

    class VLine(int x, int y1, int y2)
    {
        public int X { get; } = x;
        public int Y1 { get; } = y1;
        public int Y2 { get; } = y2;
        public int MinY => Math.Min(Y1, Y2);
        public int MaxY => Math.Max(Y1, Y2);
        public bool ContainsPoint(Point p) => p.X == X && p.Y >= MinY && p.Y <= MaxY;
    }
}
