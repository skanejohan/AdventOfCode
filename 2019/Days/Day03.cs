using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode.Days2019
{
    public static class Day03
    {
        public static int Part1()
        {
            var (wire1, wire2) = Read();
            var cells1 = CalculateAffectedCells(wire1);
            var cells2 = CalculateAffectedCells(wire2);
            var collissions = CalculateCollissionsBetween(cells1, cells2);
            return collissions.Select(ManhattanDistanceTo).Min();
        }

        public static int Part2()
        {
            var (wire1, wire2) = Read();
            var cells1 = CalculateAffectedCells(wire1);
            var cells2 = CalculateAffectedCells(wire2);
            var collissions = CalculateCollissionsBetween(cells1, cells2);


            var stepsRequired = int.MaxValue;
            foreach(var collission in collissions)
            {
                var sr = CalculateStepsToReach(wire1, collission.Item1, collission.Item2) +
                         CalculateStepsToReach(wire2, collission.Item1, collission.Item2);
                stepsRequired = Math.Min(stepsRequired, sr);
            }
            return stepsRequired;
        }

        private static int ManhattanDistanceTo((int, int) cell)
            => Math.Abs(cell.Item1) + Math.Abs(cell.Item2);

        private static IEnumerable<(int, int)> CalculateAffectedCells(IEnumerable<Movement> wire)
        {
            var posX = 0;
            var posY = 0;
            foreach (var movement in wire)
            {
                switch (movement.Direction)
                {
                    case Direction.R:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posX += 1;
                            yield return (posX, posY);
                        }
                        break;
                    case Direction.D:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posY += 1;
                            yield return (posX, posY);
                        }
                        break;
                    case Direction.L:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posX -= 1;
                            yield return (posX, posY);
                        }
                        break;
                    default:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posY -= 1;
                            yield return (posX, posY);
                        }
                        break;
                }
            }
        }

        private static int CalculateStepsToReach(IEnumerable<Movement> wire, int x, int y)
        {
            var posX = 0;
            var posY = 0;
            var steps = 0;
            var result = 0;
            foreach (var movement in wire)
            {
                switch (movement.Direction)
                {
                    case Direction.R:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posX += 1;
                            steps++;
                            if (x == posX && y == posY && result == 0)
                            {
                                result = steps;
                            }
                        }
                        break;
                    case Direction.D:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posY += 1;
                            steps++;
                            if (x == posX && y == posY && result == 0)
                            {
                                result = steps;
                            }
                        }
                        break;
                    case Direction.L:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posX -= 1;
                            steps++;
                            if (x == posX && y == posY && result == 0)
                            {
                                result = steps;
                            }
                        }
                        break;
                    default:
                        for (var i = 0; i < movement.Distance; i++)
                        {
                            posY -= 1;
                            steps++;
                            if (x == posX && y == posY && result == 0)
                            {
                                result = steps;
                            }
                        }
                        break;
                }
            }
            return result;
        }

        private static IEnumerable<(int,int)> CalculateCollissionsBetween(IEnumerable<(int,int)> wire1, IEnumerable<(int,int)> wire2)
        {
            var set = new HashSet<(int,int)>();
            foreach(var cell in wire1)
            {
                set.Add(cell);
            }
            foreach (var cell in wire2)
            {
                if (set.Contains(cell))
                {
                    yield return cell;
                }
            }
        }

        private static (IEnumerable<Movement>, IEnumerable<Movement>) Read()
        {
            var texts = DataReader.ReadAllText("Day03Input.txt").Split('\n');
            return (texts[0].Split(',').Select(s => new Movement(s)), texts[1].Split(',').Select(s => new Movement(s)));
        }

        enum Direction { R, U, L, D };
        
        class Movement
        {
            public Direction Direction;
            public int Distance;
            public Movement(string s)
            {
                switch(s[0])
                {
                    case 'R': Direction = Direction.R; break;
                    case 'D': Direction = Direction.D; break;
                    case 'L': Direction = Direction.L; break;
                    default: Direction = Direction.U; break;
                }
                Distance = int.Parse(s.Substring(1));
            }
        }
    }
}
