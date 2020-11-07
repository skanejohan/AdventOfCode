using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    internal static class Day11
    {
        private static (int, int) robotPosition;
        private static Direction robotDirection;
        private static HashSet<(int, int)> whiteCells;
        private static HashSet<(int, int)> paintedCells;
        private static bool firstOutput;

        public static long Part1()
        {
            robotPosition = (0, 0);
            robotDirection = Direction.Up;
            whiteCells = new HashSet<(int, int)>();
            paintedCells = new HashSet<(int, int)>();
            firstOutput = true;
            new IntCodeComputer(GetData(), HandleInput, HandleOutput).Run();
            return paintedCells.Count();
        }

        public static string Part2()
        {
            robotPosition = (0, 0);
            robotDirection = Direction.Up;
            whiteCells = new HashSet<(int, int)>();
            paintedCells = new HashSet<(int, int)>();
            firstOutput = true;

            whiteCells.Add((0, 0));
            paintedCells.Add((0, 0));
            new IntCodeComputer(GetData(), HandleInput, HandleOutput).Run();
            // VisualizeHull(); // This shows that the hull now displays "BCKFPCRA"
            return "BCKFPCRA";
        }

        private static long HandleInput()
        {
            // Provide 0 if the robot is over a black panel or 1 if the robot is over a white panel
            return whiteCells.Contains(robotPosition) ? 1 : 0;
        }

        private static bool HandleOutput(long v)
        {
            if (firstOutput)
            {
                // First, it will output a value indicating the color to paint the panel the robot is  
                // over: 0 means to paint the panel black, and 1 means to paint the panel white.
                paintedCells.Add(robotPosition);
                if (v == 0)
                {
                    whiteCells.Remove(robotPosition);
                }
                else
                {
                    whiteCells.Add(robotPosition);
                }
            }
            else
            {
                // Second, it will output a value indicating the direction the robot should turn: 0 means 
                // it should turn left 90 degrees, and 1 means it should turn right 90 degrees.
                if (v == 0)
                {
                    robotDirection = robotDirection.TurnLeft();
                }
                else
                {
                    robotDirection = robotDirection.TurnRight();
                }
                MoveRobot();
            }
            firstOutput = !firstOutput;
            return true;
        }

        private static void MoveRobot()
        {
            switch (robotDirection)
            {
                case Direction.Right:
                    robotPosition = (robotPosition.Item1 + 1, robotPosition.Item2);
                    break;
                case Direction.Down:
                    robotPosition = (robotPosition.Item1, robotPosition.Item2 + 1);
                    break;
                case Direction.Left:
                    robotPosition = (robotPosition.Item1 - 1, robotPosition.Item2);
                    break;
                case Direction.Up:
                    robotPosition = (robotPosition.Item1, robotPosition.Item2 - 1);
                    break;
            }
        }

        private static void VisualizeHull()
        {
            var (minX, minY, maxX, maxY) = calculateDimensions();
            for (var y = minY; y <= maxY; y++)
            {
                var line = "";
                for (var x = minX; x <= maxX; x++)
                {
                    line += whiteCells.Contains((x, y)) ? "#" : ".";
                }
                Console.WriteLine(line);
            }
        }

        private static (int xMin, int yMin, int xMax, int yMax) calculateDimensions()
            => (
                paintedCells.Select(pos => pos.Item1).Min(),
                paintedCells.Select(pos => pos.Item2).Min(),
                paintedCells.Select(pos => pos.Item1).Max(),
                paintedCells.Select(pos => pos.Item2).Max()
            );

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day11Input.txt").ToList();
    }
}
