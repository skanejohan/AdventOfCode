using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day14;

public static class Solver
{
    public static long Part1()
    {
        var width = 101;
        var height = 103;
        var steps = 100;
        var robots = LoadData("data.txt");
        MoveRobots(robots, width, height, steps);
        var q1 = robots.Where(r => r.X < width / 2 && r.Y < height / 2).Count();
        var q2 = robots.Where(r => r.X > width / 2 && r.Y < height / 2).Count();
        var q3 = robots.Where(r => r.X < width / 2 && r.Y > height / 2).Count();
        var q4 = robots.Where(r => r.X > width / 2 && r.Y > height / 2).Count();
        return q1 * q2 * q3 * q4;
    }

    public static long Part2()
    {
        // Looping through steps, I saw that after 6516 steps, there was a clear Christmas tree.
        // I manually figured out its positions, then checked the first step where many of them
        // were available. Turns out that was 6516...

        var n = 0;
        var width = 101;
        var height = 103;
        var robots = LoadData("data.txt");

        while (true)
        {
            var hs = robots.Select(r => (r.X, r.Y)).ToHashSet();
            var hasTopRow = true;
            for (var x = 41; x < 72; x++)
            {
                if (!hs.Contains((x, 41)))
                {
                    hasTopRow = false;
                    break;
                }
            }
            var hasLeftCol = true;
            for (var y = 41; y < 74; y++)
            {
                if (!hs.Contains((41, y)))
                {
                    hasLeftCol = false;
                    break;
                }
            }

            if (hasTopRow && hasLeftCol)
            {
                Console.WriteLine(n);
                foreach (var s in Print(robots, width, height))
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                break;
            }
            MoveRobots(robots, width, height, 1);
            n++;
        }

        return 6516;
    }

    static List<(long X, long Y, long DX, long DY)> LoadData(string fileName)
    {
        return new DataLoader(2024, 14).ReadStrings(fileName).Select(s => 
        {
            var r = s.Split(",").Select(long.Parse).ToList();
            return (r[0], r[1], r[2], r[3]);
        }).ToList();
    }

    static void MoveRobots(List<(long X, long Y, long DX, long DY)> robots, int width, int height, int steps)
    {
        for (var i = 0; i < robots.Count; i++)
        {
            var (X, Y, DX, DY) = robots[i];
            var x = (width + X + steps * DX) % width;
            var y = (height + Y + steps * DY) % height;
            if (x < 0) x += width;
            if (y < 0) y += height;
            robots[i] = (x, y, DX, DY);
        }
    }

    static IEnumerable<string> Print(List<(long X, long Y, long DX, long DY)> robots, int width, int height)
    {
        for (var y = 0; y < height; y++)
        {
            var s = "";
            for (var x = 0; x < width; x++)
            {
                var n = robots.Count(r => r.X == x && r.Y == y);
                s += n == 0 ? "." : $"{n}";
            }
            yield return s;
        }
    }
}
