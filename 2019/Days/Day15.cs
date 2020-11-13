using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    internal static class Day15
    {
        public static long Part1()
        {
            return 0;
        }

        public static long Part2()
        {
            return 0;
        }

        static Day15()
        {
            Debug.DebugMode = Debug.Mode.MazeChanges;
            GenerateMaze();
        }

        private static long Input()
        {
            var attempt = Attempts.Pop().Where(dir => !Visited.Contains(Compass.PositionAfterMovement(DroidPosition, dir))).ToList();
            if (attempt.Count == 0)
            {
                Backtracking = true;
                Attempt = Compass.Opposite(EnteredFrom[DroidPosition]);
            }
            else
            {
                Backtracking = false;
                Attempt = attempt[0];
                attempt.RemoveAt(0);
                Attempts.Push(attempt);
            }
            var backTrackingString = Backtracking ? " (BACKTRACKING)" : "";
            Debug.Write($"({DroidPosition.x}),{DroidPosition.y}) - {Compass.Name(Attempt)}{backTrackingString} : ");
            return Compass.ToLong(Attempt);
        }

        private static bool Output(long status)
        {
            var previousPosition = DroidPosition;
            var attemptedPosition = Compass.PositionAfterMovement(DroidPosition, Attempt);
            if (status == 0)
            {
                // The repair droid hit a wall. Its position has not changed.
                Maze.Set(attemptedPosition, Maze.Cell.Wall);
                Debug.WriteLine("BLOCKED");
            }
            else
            {
                // The repair droid has moved one step in the requested direction.
                Debug.WriteLine("OK");
                DroidPosition = attemptedPosition;
                if (!Backtracking)
                {
                    Maze.Set(attemptedPosition, Maze.Cell.Open);
                    if (status == 2)
                    {
                        OxygenSystemPosition = attemptedPosition;
                    }
                    EnteredFrom[attemptedPosition] = Attempt;
                    Visited.Add(attemptedPosition);
                    Attempts.Push(Compass.AllDirections);
                }
            }

            return (Attempts.Count() > 1) || (Attempts.Peek().Count > 0);
        }

        private static void GenerateMaze()
        {
            Attempts.Push(Compass.AllDirections);
            new IntCodeComputer(GetData(), Input, Output).Run();
        }

        private static (int x, int y) DroidPosition = (0, 0);
        private static (int x, int y) OxygenSystemPosition;
        private static Compass.Direction Attempt;
        private static Stack<List<Compass.Direction>> Attempts = new Stack<List<Compass.Direction>>();
        private static Dictionary<(int x, int y), Compass.Direction> EnteredFrom = new Dictionary<(int x, int y), Compass.Direction>();
        private static HashSet<(int x, int y)> Visited = new HashSet<(int, int)> { DroidPosition };
        private static bool Backtracking = false;

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day15Input.txt").ToList();
    }

    internal static class Compass
    {
        public enum Direction { North, East, South, West };

        public static long ToLong(Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return 1;
                case Direction.East: return 4;
                case Direction.South: return 2;
                default: return 3;
            }
        }

        public static List<Direction> AllDirections => new List<Direction>
        {
            Direction.North, Direction.West, Direction.South, Direction.East
        };

        public static string Name(Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return "N";
                case Direction.East: return "E";
                case Direction.South: return "S";
                default: return "W";
            }
        }

        public static Direction Opposite(Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return Direction.South;
                case Direction.East: return Direction.West;
                case Direction.South: return Direction.North;
                default: return Direction.East;
            }
        }

        public static (int x, int y) PositionAfterMovement((int x, int y) position, Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return (position.x, position.y - 1);
                case Direction.East: return (position.x + 1, position.y);
                case Direction.South: return (position.x, position.y + 1);
                default: return (position.x - 1, position.y);
            }
        }
    }

    internal static class Maze
    {
        public enum Cell { Unexplored, Wall, Open }

        public static int MinX { get; private set; } = 0;
        public static int MinY { get; private set; } = 0;
        public static int MaxX { get; private set; } = 0;
        public static int MaxY { get; private set; } = 0;

        public static void Set((int x, int y) pos, Cell cell)
        {
            area[pos] = cell;
            MinX = Math.Min(MinX, pos.x);
            MinY = Math.Min(MinY, pos.y);
            MaxX = Math.Max(MaxX, pos.x);
            MaxY = Math.Max(MaxY, pos.y);
            Debug.Set(pos, cell);
        }

        public static Cell Get((int x, int y) pos)
        {
            if (!area.TryGetValue(pos, out Cell c))
            {
                return Cell.Unexplored;
            }
            return c;
        }

        private static readonly Dictionary<(int x, int y), Cell> area = new Dictionary<(int x, int y), Cell>();
    }

    internal static class Debug
    {
        public enum Mode { None, Operations, MazeChanges }

        public static Mode DebugMode;

        public static void Set((int x, int y) pos, Maze.Cell cell)
        {
            if (DebugMode == Mode.MazeChanges)
            {
                DrawColor(pos, CellColors[cell]);
            }
        }

        public static void Write(string s)
        {
            if (DebugMode == Mode.Operations)
            {
                Console.Write(s);
            }
        }

        public static void WriteLine(string s)
        {
            if (DebugMode == Mode.Operations)
            {
                Console.WriteLine(s);
            }
        }

        private static void DrawColor((int x, int y) pos, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.SetCursorPosition(pos.x + 40, pos.y + 40);
            Console.Write(' ');
        }

        private static Dictionary<Maze.Cell, ConsoleColor> CellColors = new Dictionary<Maze.Cell, ConsoleColor>
        {
            { Maze.Cell.Open, ConsoleColor.Yellow },
            { Maze.Cell.Unexplored, ConsoleColor.Black },
            { Maze.Cell.Wall, ConsoleColor.Red },
        };
    }
}
