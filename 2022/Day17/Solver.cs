using CSharpLib;
using CSharpLib.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day17
{
    public static class Solver
    {
        public static long Part1()
        {
            jetPattern = LoadData("data.txt");
            jetPatternIndex = 0;
            chamber = CreateChamber(7);
            rockPattern = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

            for (var i = 0; i < 2022; i++)
            {
                var rockIndex = i % 5;
                var rock = GetRock(rockIndex, 2, rockPattern.Max() + 3);
                DropRock(rock);
            }

            return rockPattern.Max();
        }

        public static long Part2()
        {
            jetPattern = LoadData("data.txt");
            jetPatternIndex = 0;
            chamber = CreateChamber(7);
            rockPattern = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

            int ctr = 0;
            int rockIndex;
            string pattern;
            int cycleLength;
            int cycleHeightDiff;
            var visitedPatterns = new HashSet<string>();
            var patternVisitedAt = new Dictionary<string, (int Ctr, int H)>();
            while(true)
            {
                rockIndex = ctr % 5;
                pattern = calculatePattern();
                if (!visitedPatterns.Contains(pattern))
                {
                    visitedPatterns.Add(pattern);
                    patternVisitedAt.Add(pattern, (ctr, rockPattern.Max()));
                    var rock = GetRock(rockIndex, 2, rockPattern.Max() + 3);
                    DropRock(rock);
                    ctr++;
                }
                else 
                {
                    cycleLength = ctr - patternVisitedAt[pattern].Ctr;
                    cycleHeightDiff = rockPattern.Max() - patternVisitedAt[pattern].H;
                    break;
                }
            }

            var additionalCycles = 1000000000000 / cycleLength - 2;
            var additionalHeight = additionalCycles * cycleHeightDiff;
            var totalRocksDropped = ctr + additionalCycles * cycleLength;
            var remainingRocks = 1000000000000 - totalRocksDropped;

            for (var i = 0; i < remainingRocks; i++)
            {
                rockIndex = ctr % 5;
                var rock = GetRock(rockIndex, 2, rockPattern.Max() + 3);
                DropRock(rock);
                ctr++;
            }

            return rockPattern.Max() + additionalHeight;

            string calculatePattern()
            {
                var minHeight = rockPattern.Min();
                var normalizedRockPattern = string.Join(',', rockPattern.Select(h => h - minHeight));
                return $"{rockIndex},{jetPatternIndex},{normalizedRockPattern}";
            }
        }

        private static List<char> LoadData(string fileName)
        {
            return new DataLoader(2022, 17).ReadStrings(fileName).First().ToList();
        }

        private static void DropRock(Rock rock)
        {
            while (true)
            {
                // Blowin' in the wind
                if (jetPattern[jetPatternIndex] == '<' && CanMoveLeft(rock))
                {
                    rock = rock with { BottomLeft = new Pos(rock.BottomLeft.X - 1, rock.BottomLeft.Y) };
                }
                else if (jetPattern[jetPatternIndex] == '>' && CanMoveRight(rock))
                {
                    rock = rock with { BottomLeft = new Pos(rock.BottomLeft.X + 1, rock.BottomLeft.Y) };
                }
                jetPatternIndex = (jetPatternIndex + 1) % jetPattern.Count;

                // Falling Down
                if (CanMoveDown(rock))
                {
                    rock = rock with { BottomLeft = new Pos(rock.BottomLeft.X, rock.BottomLeft.Y - 1) };
                }
                else
                {
                    foreach (var cell in rock.Cells)
                    {
                        chamber.Add(rock.BottomLeft.X + cell.X, rock.BottomLeft.Y + cell.Y, '#');
                    }
                    break;
                }
            }
            foreach (var cell in rock.Cells)
            {
                var x = rock.BottomLeft.X + cell.X;
                var y = rock.BottomLeft.Y + cell.Y + 1;
                rockPattern[x] = y > rockPattern[x] ? y : rockPattern[x];
            }
        }

        private static bool CanMoveLeft(Rock rock)
        {
            foreach (var cell in rock.BlockLeft)
            {
                var x = rock.BottomLeft.X + cell.X;
                var y = rock.BottomLeft.Y + cell.Y;
                if (x == 0 || chamber.Has(x - 1, y))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CanMoveRight(Rock rock)
        {
            foreach (var cell in rock.BlockRight)
            {
                var x = rock.BottomLeft.X + cell.X;
                var y = rock.BottomLeft.Y + cell.Y;
                if (x == chamber.MaxX || chamber.Has(x + 1, y))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CanMoveDown(Rock rock)
        {
            foreach (var cell in rock.BlockDown)
            {
                var x = rock.BottomLeft.X + cell.X;
                var y = rock.BottomLeft.Y + cell.Y;
                if (y == 0 || chamber.Has(x, y - 1))
                {
                    return false;
                }
            }
            return true;
        }

        private static void WriteChamber(Rock? rock = null)
        {
            if (rock != null)
            {
                foreach (var cell in rock.Cells)
                {
                    chamber.Add(rock.BottomLeft.X + cell.X, rock.BottomLeft.Y + cell.Y, '@');
                }
            }
            Console.WriteLine(chamber.ToStringUpsideDown());
            if (rock != null)
            {
                foreach (var cell in rock.Cells)
                {
                    chamber.Remove(rock.BottomLeft.X + cell.X, rock.BottomLeft.Y + cell.Y);
                }
            }
        }

        private static InfiniteGrid<char> CreateChamber(int width)
        {
            var chamber = new InfiniteGrid<char>('.');
            for (var i = 0; i < width; i++)
            {
                chamber.Add(i, -1, '-');
            }
            return chamber;
        }

        private static List<char> jetPattern;
        private static int jetPatternIndex;
        private static InfiniteGrid<char> chamber;
        private static int[] rockPattern;

        record Pos(int X, int Y);
        record Rock(Pos BottomLeft, List<Pos> Cells, List<Pos> BlockLeft, List<Pos> BlockDown, List<Pos> BlockRight, int Height);
        
        private static Rock GetRock(int index, int x, int y)
        {
            var factories = new Func<Pos, Rock>[] { Minus, Plus, J, I, Square };
            return factories[index](new Pos(x, y));
        }

        // ####
        private static Rock Minus(Pos bottomLeft)
        {
            return new Rock(bottomLeft,
                Cells: new List<Pos> { new Pos(0, 0), new Pos(1, 0), new Pos(2, 0), new Pos(3, 0) },
                BlockLeft: new List<Pos> { new Pos(0, 0) },
                BlockDown: new List<Pos> { new Pos(0, 0), new Pos(1, 0), new Pos(2, 0), new Pos(3, 0) },
                BlockRight: new List<Pos> { new Pos(3, 0) },
                Height: 1);
        }

        // .#.
        // ###
        // .#.
        private static Rock Plus(Pos bottomLeft)
        {
            return new Rock(bottomLeft,
                Cells: new List<Pos> { new Pos(1, 0), new Pos(0, 1), new Pos(1, 1), new Pos(2, 1), new Pos(1, 2) },
                BlockLeft: new List<Pos> { new Pos(1, 0), new Pos(0, 1), new Pos(1, 2) },
                BlockDown: new List<Pos> { new Pos(0, 1), new Pos(1, 0), new Pos(2, 1) },
                BlockRight: new List<Pos> { new Pos(1, 0), new Pos(2, 1), new Pos(1, 2) },
                Height: 3);
        }

        // ..#
        // ..#
        // ###
        private static Rock J(Pos bottomLeft)
        {
            return new Rock(bottomLeft,
                Cells: new List<Pos> { new Pos(0, 0), new Pos(1, 0), new Pos(2, 0), new Pos(2, 1), new Pos(2, 2) },
                BlockLeft: new List<Pos> { new Pos(0, 0), new Pos(2, 1), new Pos(2, 2) },
                BlockDown: new List<Pos> { new Pos(0, 0), new Pos(1, 0), new Pos(2, 0) },
                BlockRight: new List<Pos> { new Pos(2, 0), new Pos(2, 1), new Pos(2, 2) },
                Height: 3);
        }

        // #
        // #
        // #
        // #
        private static Rock I(Pos bottomLeft)
        {
            return new Rock(bottomLeft,
                Cells: new List<Pos> { new Pos(0, 0), new Pos(0, 1), new Pos(0, 2), new Pos(0, 3) },
                BlockLeft: new List<Pos> { new Pos(0, 0), new Pos(0, 1), new Pos(0, 2), new Pos(0, 3) },
                BlockDown: new List<Pos> { new Pos(0, 0) },
                BlockRight: new List<Pos> { new Pos(0, 0), new Pos(0, 1), new Pos(0, 2), new Pos(0, 3) },
                Height: 4);
        }

        // ##
        // ##
        private static Rock Square(Pos bottomLeft)
        {
            return new Rock(bottomLeft,
                Cells: new List<Pos> { new Pos(0, 0), new Pos(1, 0), new Pos(0, 1), new Pos(1, 1) },
                BlockLeft: new List<Pos> { new Pos(0, 0), new Pos(0, 1) },
                BlockDown: new List<Pos> { new Pos(0, 0), new Pos(1, 0) },
                BlockRight: new List<Pos> { new Pos(1, 0), new Pos(1, 1) },
                Height: 2);
        }
    }
}
