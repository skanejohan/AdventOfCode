using CSharpLib;
using CSharpLib.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day23
{
    public static class Solver
    {
        public static long Part1()
        {
            LoadData("data.txt");
            for (var i = 0; i < 10; i++)
            {
                PerformOneRound(i);
            }
            return (ground.MaxX - ground.MinX + 1) * (ground.MaxY - ground.MinY + 1) - ground.Count(); // 2332 is too low
        }

        public static long Part2()
        {
            LoadData("data.txt");
            var i = 0;
            while (PerformOneRound(i++) != 0)
            {
            }
            return i;
        }

        private static int PerformOneRound(int index)
        {
            var moved = 0;
            var targets = new CountedSet<(int, int)>();
            var moves = new Dictionary<(int, int), (int, int)>();
            foreach (var (X, Y, _) in ground)
            {
                if (Suggest((X, Y), index % 4, out var to))
                {
                    moves.Add((X, Y), to);
                    targets.Add(to);
                }
            }
            foreach (var move in moves)
            {
                if (targets.Occurs(move.Value) == 1)
                {
                    ground.Remove(move.Key.Item1, move.Key.Item2);
                    ground.Set(move.Value.Item1, move.Value.Item2, '#');
                    moved++;
                }
            }
            return moved;
        }

        private static bool Suggest((int X, int Y) from, int index, out (int X, int Y) to)
        {
            var nw = ground.Has(from.X - 1, from.Y - 1);
            var n = ground.Has(from.X, from.Y - 1);
            var ne = ground.Has(from.X + 1, from.Y - 1);
            var w = ground.Has(from.X - 1, from.Y);
            var e = ground.Has(from.X + 1, from.Y);
            var sw = ground.Has(from.X - 1, from.Y + 1);
            var s = ground.Has(from.X, from.Y + 1);
            var se = ground.Has(from.X + 1, from.Y + 1);

            to = (0, 0);

            if (!(nw || n || ne || w || e || sw || s || se))
            {
                return false;
            }

            return index switch
            {
                0 => N(ref to) || S(ref to) || W(ref to) || E(ref to),
                1 => S(ref to) || W(ref to) || E(ref to) || N(ref to),
                2 => W(ref to) || E(ref to) || N(ref to) || S(ref to),
                _ => E(ref to) || N(ref to) || S(ref to) || W(ref to),
            };

            bool N(ref (int, int) to)
            {
                if (!(nw || n || ne))
                {
                    to = (from.X, from.Y - 1);
                    return true;
                }
                return false;
            }

            bool E(ref (int, int) to)
            {
                if (!(ne || e || se))
                {
                    to = (from.X + 1, from.Y);
                    return true;
                }
                return false;
            }

            bool W(ref (int, int) to)
            {
                if (!(nw || w || sw))
                {
                    to = (from.X - 1, from.Y);
                    return true;
                }
                return false;
            }

            bool S(ref (int, int) to)
            {
                if (!(sw || s || se))
                {
                    to = (from.X, from.Y + 1);
                    return true;
                }
                return false;
            }
        }

        private static InfiniteGrid<char> ground = new('.');

        private static void LoadData(string fileName)
        {
            ground = new InfiniteGrid<char>();
            var y = 0;
            foreach (var line in new DataLoader(2022, 23).ReadStrings(fileName))
            {
                var x = 0;
                foreach (var c in line)
                {
                    if (c == '#')
                    {
                        ground.Set(x, y, c);
                    }
                    x++;
                }
                y++;
            }
        }
    }
}
