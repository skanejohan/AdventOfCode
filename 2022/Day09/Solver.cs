using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day09
{
    public static class Solver
    {
        public static long Part1()
        {
            var moves = LoadData("data.txt");
            return Solve(2, moves);
        }

        public static long Part2()
        {
            var moves = LoadData("data.txt");
            return Solve(10, moves);
        }

        private static long Solve(int ropeLength, IEnumerable<(char C, int N)> moves)
        {
            var visited = new HashSet<(int, int)>();
            var rope = CreateRope(ropeLength);
            foreach (var (C, N) in moves)
            {
                for (int i = 0; i < N; i++)
                {
                    Move(rope, C);
                    visited.Add(rope[ropeLength - 1]);
                }
            }
            return visited.Count;
        }

        private static void Move((int X, int Y)[] rope, char dir)
        {
            var dx = dir == 'R' ? 1 : dir == 'L' ? -1 : 0;
            var dy = dir == 'U' ? 1 : dir == 'D' ? -1 : 0;

            rope[0].X += dx;
            rope[0].Y += dy;

            for (int i = 1; i < rope.Length; i++)
            {
                if (rope[i - 1].X - rope[i].X == 2)
                {
                    rope[i].X += 1;
                    rope[i].Y += Math.Sign(rope[i - 1].Y - rope[i].Y);
                }
                else if (rope[i].X - rope[i - 1].X == 2)
                {
                    rope[i].X -= 1;
                    rope[i].Y += Math.Sign(rope[i - 1].Y - rope[i].Y);
                }
                else if (rope[i - 1].Y - rope[i].Y == 2)
                {
                    rope[i].Y += 1;
                    rope[i].X += Math.Sign(rope[i - 1].X - rope[i].X);
                }
                else if (rope[i].Y - rope[i - 1].Y == 2)
                {
                    rope[i].Y -= 1;
                    rope[i].X += Math.Sign(rope[i - 1].X - rope[i].X);
                }
            }
        }

        private static (int X, int Y)[] CreateRope(int length)
        {
            var rope = new (int X, int Y)[length];
            for (int i = 0; i < length; i++)
            {
                rope[i] = (0, 0);
            }
            return rope;
        }

        private static IEnumerable<(char C, int N)> LoadData(string fileName)
        {
            return new DataLoader(2022, 9).ReadStrings(fileName).Select(s => 
            {
                return (s[0], int.Parse(s[2..]));
            });
        }
    }
}
