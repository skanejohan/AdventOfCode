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

        private static long Solve(string fileName, Func<int, int, ((int X, int Y) Pos, Facing F)> nextFn)
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

        private static void Move(Func<int, int, ((int X, int Y) Pos, Facing F)> nextFn)
        {
            var next = facing switch
            {
                Facing.Right => nextFn(1, 0),
                Facing.Down => nextFn(0, 1),
                Facing.Left => nextFn(-1, 0),
                Facing.Up => nextFn(0, -1)
            };
            if (map[next.Pos] != '#')
            {
                pos = next.Pos;
                facing = next.F;
            }
        }

        private static ((int X, int Y) Pos, Facing F) Next1(int dx, int dy)
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
            return (next, facing);
        }

        private static ((int X, int Y) Pos, Facing F) Next2(int dx, int dy)
        {
            const int side = 50;
            var f = facing;
            var next = (X: pos.X + dx, Y: pos.Y + dy);
            if (!map.ContainsKey(next) || map[next] == ' ')
            {
                if (facing == Facing.Right)
                {
                    if (next.Y <= side) // Leave face 2 facing right, enter face 4 facing left
                    {
                        next.X = 2 * side;
                        next.Y = 3 * side + 1 - next.Y;
                        f = Facing.Left;
                    }
                    else if (next.Y <= 2 * side) // Leave face 3 facing right, enter face 2 facing up
                    {
                        next.X = next.Y + side;
                        next.Y = side;
                        f = Facing.Up;
                    }
                    else if (next.Y <= 3 * side) // Leave face 4 facing right, enter face 2 facing left
                    {
                        next.Y = 3 * side + 1 - next.Y;
                        next.X = 3 * side;
                        f = Facing.Left;
                    }
                    else // Leave face 6 facing right, enter face 4 facing up
                    {
                        next.X = next.Y - 2 * side;
                        next.Y = 3 * side;
                        f = Facing.Up;
                    }
                }
                else if (facing == Facing.Up)
                {
                    if (next.X <= side) // Leave face 5 facing up, enter face 3 facing right
                    {
                        next.Y = next.X + side;
                        next.X = side + 1;
                        f = Facing.Right;
                    }
                    else if (next.X <= 2 * side) // Leave face 1 facing up, enter face 6 facing right
                    {
                        next.Y = next.X + 2 * side;
                        next.X = 1;
                        f = Facing.Right;
                    }
                    else // Leave face 2 facing up, enter face 6 facing up
                    {
                        next.Y = 4 * side;
                        next.X = next.X - 2 * side;
                        f = Facing.Up;
                    }
                }
                else if (facing == Facing.Left)
                {
                    if (next.Y <= side) // Leave face 1 facing left, enter face 5 facing right
                    {
                        next.X = 1;
                        next.Y = 3 * side + 1 - next.Y;
                        f = Facing.Right;
                    }
                    else if (next.Y <= 2 * side) // Leave face 3 facing left, enter face 5 facing down
                    {
                        next.X = next.Y - side;
                        next.Y = 2 * side + 1;
                        f = Facing.Down;
                    }
                    else if (next.Y <= 3 * side) // Leave face 5 facing left, enter face 1 facing right
                    {
                        next.X = side + 1;
                        next.Y = 3 * side + 1 - next.Y;
                        f = Facing.Right;
                    }
                    else // Leave face 6 facing left, enter face 1 facing down
                    {
                        next.X = next.Y - 2 * side;
                        next.Y = 1;
                        f = Facing.Down;
                    }
                }
                else // Facing.Down
                {
                    if (next.X <= side) // Leave face 6 facing down, enter face 2 facing down
                    {
                        next.X = next.X + 2 * side;
                        next.Y = 1;
                        f = Facing.Down;
                    }
                    else if (next.X <= 2 * side) // Leave face 4 facing down, enter face 6 facing left
                    {
                        next.Y = next.X + 2 * side;
                        next.X = side;
                        f = Facing.Left;
                    }
                    else // Leave face 2 facing down, enter face 3 facing left
                    {
                        next.Y = next.X - side;
                        next.X = 2 * side;
                        f = Facing.Left;
                    }
                }
            }
            return (next, f);
        }

        private static ((int X, int Y) Pos, Facing F) Next2TestData(int dx, int dy)
        {
            var f = facing;
            var next = (X: pos.X + dx, Y: pos.Y + dy);
            if (!map.ContainsKey(next) || map[next] == ' ')
            {
                if (facing == Facing.Right) 
                {
                    
                    if (next.Y < 5) // Leave face 1 facing right, enter face 6 facing left
                    {
                        next.X = 16;
                        next.Y = 13 - next.Y;
                        f = Facing.Left;
                    }
                    else if (next.Y < 9) // Leave face 4 facing right, enter face 6 facing down
                    {
                        next.X = 21 - next.Y;
                        next.Y = 9;
                        f = Facing.Down;
                    }
                    else // Leave face 6 facing right, enter face 1 facing left
                    {
                        next.X = 12;
                        next.Y = 13 - next.Y;
                        f = Facing.Left;
                    }
                }
                else if (facing == Facing.Up)
                {
                    if (next.X < 5) // Leave face 2 facing up, enter face 1 facing down
                    {
                        next.X = 13 - next.X;
                        next.Y = 1;
                        f = Facing.Down;
                    }
                    else if (next.X < 9) // Leave face 3 facing up, enter face 1 facing right
                    {
                        next.Y = next.X - 4;
                        next.X = 9;
                        f = Facing.Right;
                    }
                    else if (next.X < 13) // Leave face 1 facing up, enter face 2 facing down
                    {
                        next.X = 13 - next.X;
                        next.Y = 5;
                        f = Facing.Down;
                    }
                    else // Leave face 6 facing up, enter face 4 facing left
                    {
                        next.Y = 21 - next.X;
                        next.X = 12;
                        f = Facing.Left;
                    }
                }
                else if (facing == Facing.Left)
                {
                    if (next.Y < 5) // Leave face 1 facing left, enter face 3 facing down
                    {
                        next.X = next.Y + 4;
                        next.Y = 5;
                        f = Facing.Down;
                    }
                    else if (next.Y < 9) // Leave face 2 facing left, enter face 6 facing up
                    {
                        next.X = 21 - next.Y;
                        next.Y = 12;
                        f = Facing.Up;
                    }
                    else // Leave face 5 facing left, enter face 3 facing up
                    {
                        next.X = 17 - next.Y;
                        next.Y = 8;
                        f = Facing.Up;
                    }
                }
                else // Facing.Down
                {
                    if (next.X < 5) // Leave face 2 facing down, enter face 5 facing up
                    {
                        next.X = 13 - next.X;
                        next.Y = 12;
                        f= Facing.Up;
                    }
                    else if (next.X < 9) // Leave face 3 facing down, enter face 5 facing right
                    {
                        next.Y = 17 - next.X;
                        next.X = 9;
                        f = Facing.Right;
                    }
                    else if (next.X < 13) // Leave face 5 facing down, enter face 2 facing up
                    {
                        next.X = 13 - next.X;
                        next.Y = 8;
                        f = Facing.Up;
                    }
                    else // Leave face 6 facing down, enter face 2 facing right
                    {
                        next.Y = 21 - next.X;
                        next.X = 1;
                        f = Facing.Right;
                    }
                }
            }
            return (next, f);
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
