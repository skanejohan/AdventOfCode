using CSharpLib;
using CSharpLib.Algorithms;
using CSharpLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard = System.Collections.Generic.List<(int X, int Y, char C)>;

namespace Y2022.Day24
{
    public static class Solver
    {
        public static long Part1()
        {
            PrepareData("data.txt");

            bool targetReached((int x, int y, int s) cell) => cell.y == blizzardsHeight + 1;
            var dijkstra = new Dijkstra<(int X, int Y, int Step)>(targetReached);
            return dijkstra.Solve((1, 0, 0), (blizzardsWidth, blizzardsHeight + 1, 0), CalculateNeighbors);
        }

        public static long Part2()
        {
            PrepareData("data.txt");

            var originX = 1;
            var originY = 0;
            var targetX = blizzardsWidth;
            var targetY = blizzardsHeight + 1;

            bool originReached((int x, int y, int s) cell) => cell.y == originY;
            bool targetReached((int x, int y, int s) cell) => cell.y == targetY;

            var steps1 = new Dijkstra<(int X, int Y, int Step)>(targetReached)
                .Solve((originX, originY, 0), (targetX, targetY, 0), CalculateNeighbors);

            var steps2 = new Dijkstra<(int X, int Y, int Step)>(originReached)
                .Solve((targetX, targetY, (int)steps1), (originX, originY, 0), CalculateNeighbors);

            var steps3 = new Dijkstra<(int X, int Y, int Step)>(targetReached)
                .Solve((originX, originY, (int)(steps1 + steps2)), (targetX, targetY, 0), CalculateNeighbors);

            return steps1 + steps2 + steps3;
        }

        private static IEnumerable<((int X, int Y, int Step), long)> CalculateNeighbors((int X, int Y, int Step) state)
        {
            var newStep = state.Step + 1;
            var blizzard = blizzards[newStep % blizzards.Count];

            if (IsFree(state.X, state.Y))
            {
                yield return ((state.X, state.Y, newStep), 1);
            }
            if (state.Y > 0 && IsFree(state.X, state.Y - 1))
            {
                yield return ((state.X, state.Y - 1, newStep), 1);
            }
            if (state.X > 0 && IsFree(state.X - 1, state.Y))
            {
                yield return ((state.X - 1, state.Y, newStep), 1);
            }
            if (state.Y < blizzardsHeight + 1 && IsFree(state.X, state.Y + 1))
            {
                yield return ((state.X, state.Y + 1, newStep), 1);
            }
            if (state.X < blizzardsWidth + 1 && IsFree(state.X + 1, state.Y))
            {
                yield return ((state.X + 1, state.Y, newStep), 1);
            }

            bool IsFree(int x, int y)
            {
                return !blizzard.Any(c => c.X == x && c.Y == y);
            }
        }

        private static void Print(string caption, Blizzard blizzard)
        {
            Console.WriteLine(caption);
            for (var y = 0; y <= blizzardsHeight + 1; y++)
            {
                for (var x = 0; x <= blizzardsWidth + 1; x++)
                {
                    var values = blizzard.Where(c => c.X == x && c.Y == y);
                    Console.Write(values.Count() switch
                    {
                        0 => ".",
                        1 => values.First().C,
                        _ => values.Count()
                    });
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static List<Blizzard> blizzards = new();
        private static int blizzardsWidth;
        private static int blizzardsHeight;

        private static void PrepareData(string fileName)
        {
            blizzards.Clear();
            
            var blizzard = new Blizzard();

            var y = 0;
            var x = 0;
            foreach (var line in new DataLoader(2022, 24).ReadStrings(fileName))
            {
                x = 0;
                foreach (var c in line)
                {
                    if (c != '.')
                    {
                        blizzard.Add((x, y, c));
                    }
                    x++;
                }
                y++;
            }

            blizzardsWidth = x - 2;
            blizzardsHeight = y - 2;

            blizzards.Add(blizzard);
            for (var i = 1; i < MathUtils.Lcm(blizzardsWidth, blizzardsHeight); i++)
            {
                blizzard = Step(blizzard);
                blizzards.Add(blizzard);
            }

            Blizzard Step(Blizzard blizzard)
            {
                return blizzard.Select(cell => cell.C switch
                {
                    '>' => (cell.X == blizzardsWidth ? 1 : cell.X + 1, cell.Y, cell.C),
                    '<' => (cell.X == 1 ? blizzardsWidth : cell.X - 1, cell.Y, cell.C),
                    'v' => (cell.X, cell.Y == blizzardsHeight ? 1 : cell.Y + 1, cell.C),
                    '^' => (cell.X, cell.Y == 1 ? blizzardsHeight : cell.Y - 1, cell.C),
                    '#' => (cell.X, cell.Y, '#')
                }).ToList();
            }
        }
    }
}
