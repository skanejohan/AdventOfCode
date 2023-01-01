using CSharpLib;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day22
{
    public static class Solver
    {
        public static long Part1()
        {
            return Solve("data.txt", Next1);
        }

        public static long Part2()
        {
            return Solve("data.txt", Next2);
        }

        private static long Solve(string fileName, Func<int, int, (int, int)> nextFn)
        {
            LoadData(fileName);
            foreach (var (Count, Rotation) in moves)
            {
                Rotate(Rotation);
                for (var i = 0; i < Count; i++)
                {
                    Move(nextFn);
                }
            }
            return (long)(1000 * pos.Y + 4 * pos.X + facing);
        }

        private static void Move(Func<int, int, (int, int)> nextFn)
        {
            var next = facing switch
            {
                Facing.Right => nextFn(1, 0),
                Facing.Down => nextFn(0, 1),
                Facing.Left => nextFn(-1, 0),
                Facing.Up => nextFn(0, -1)
            };
            if (map[next] != '#')
            {
                pos = next;
            }
        }

        private static (int X, int Y) Next1(int dx, int dy)
        {
            var next = (X: pos.X + dx, Y: pos.Y + dy);
            if (!map.ContainsKey(next) || map[next] == ' ')
            {
                next = pos;
                while (map.ContainsKey(next) && map[next] != ' ')
                {
                    next = (next.X - dx, next.Y - dy);
                }
                next = (next.X + dx, next.Y + dy);
            }
            return next;
        }

        private static (int X, int Y) Next2(int dx, int dy)
        {
            throw new NotImplementedException();
        }

        private static void Rotate(char rotation)
        {
            if (rotation == 'R')
            {
                facing = facing == Facing.Up ? Facing.Right : facing + 1;
            }
            else if (rotation == 'L')
            {
                facing = facing == Facing.Right ? Facing.Up : facing - 1;
            }
        }

        private static void LoadData(string fileName)
        {
            var data = new DataLoader(2022, 22).ReadStrings(fileName).ChunkBy(s => s == "").ToList();
            pos = (data[0].First().IndexOf('.') + 1, 1);

            var y = 1;
            map.Clear();
            foreach (var line in data[0])
            {
                var x = 1;
                foreach (var c in line)
                {
                    map[(x, y)] = c;
                    x++;
                }
                y++;
            }

            moves.Clear();
            var numStr = "";
            var direction = 'R';
            var movesLine = data[1].First();
            for (int i = 0; i < movesLine.Length; i++)
            {
                if (char.IsDigit(movesLine[i]))
                {
                    numStr += movesLine[i];
                }
                else
                {
                    moves.Add((int.Parse(numStr), direction));
                    direction = movesLine[i];
                    numStr = "";
                }
            }
            moves.Add((int.Parse(numStr), direction));

            facing = Facing.Up; // Will start by rotating 'R' to right
        }

        private enum Facing
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3,
        }

        private static readonly Dictionary<(int X, int Y), char> map = new();
        private static readonly List<(int Count, char Rotation)> moves = new();
        private static (int X, int Y) pos;
        private static Facing facing;
    }
}
