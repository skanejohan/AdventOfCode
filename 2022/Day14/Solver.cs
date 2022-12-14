using CSharpLib;
using CSharpLib.DataStructures;
using System;

namespace Y2022.Day14
{
    public static class Solver
    {
        public static long Part1()
        {
            var grid = LoadData("data.txt");
            var dropped = 0;
            while (true)
            {
                var (x, y) = Drop(500, 0, grid);
                (x, y) = Trickle(x, y, grid);
                if (y > grid.MaxY)
                {
                    break;
                }
                grid.Add(x, y, 'o');
                dropped++;
            }
            return dropped;
        }

        public static long Part2()
        {
            var grid = LoadData("data.txt");
            var (x, y) = (0, 0);
            var dropped = 0;
            while ((x, y) != (500, 0))
            {
                (x, y) = Drop(500, 0, grid);
                (x, y) = Trickle(x, y, grid);
                grid.Add(x, y, 'o');
                dropped++;
            }
            return dropped;
        }

        private static (int X, int Y) Drop(int x, int y, InfiniteGrid<char> grid)
        {
            while (true)
            {
                if (grid.Has(x, y + 1) || y > grid.MaxY)
                {
                    break;
                }
                y++;
            }
            return (x, y);
        }

        private static (int X, int Y) Trickle(int x, int y, InfiniteGrid<char> grid)
        {
            while (y <= grid.MaxY)
            {
                if (!grid.Has(x, y + 1))
                {
                    return Trickle(x, y + 1, grid);
                }

                if (!grid.Has(x - 1, y + 1))
                {
                    return Trickle(x - 1, y + 1, grid);
                }

                if (!grid.Has(x + 1, y + 1))
                {
                    return Trickle(x + 1, y + 1, grid);
                }

                return (x, y);
            }
            return (x, y);
        }

        private static InfiniteGrid<char> LoadData(string fileName)
        {
            var grid = new InfiniteGrid<char>('.');
            var data = new DataLoader(2022, 14).ReadStrings(fileName);
            foreach (var line in data)
            {
                var coords = line.Split(" -> ");
                for (int i = 1; i < coords.Length; i++)
                {
                    var dimensions = coords[i - 1].Split(',');
                    var x1 = int.Parse(dimensions[0]);
                    var y1 = int.Parse(dimensions[1]);
                    dimensions = coords[i].Split(',');
                    var x2 = int.Parse(dimensions[0]);
                    var y2 = int.Parse(dimensions[1]);
                    if (x1 == x2)
                    {
                        for (var y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
                        {
                            if (!grid.Has(x1, y))
                            {
                                grid.Add(x1, y, '#');
                            }
                        }
                    }
                    else
                    {
                        for (var x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
                        {
                            if (!grid.Has(x, y1))
                            {
                                grid.Add(x, y1, '#');
                            }
                        }
                    }
                }
            }

            // Add the "infinite" floor
            var yFloor = grid.MaxY + 2;
            var minX = grid.MinX - 10000;
            var maxX = grid.MaxX + 10000;
            for (var x = minX; x < maxX; x++)
            {
                grid.Add(x, yFloor, '#');
            }

            return grid;
        }
    }
}
