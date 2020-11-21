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
            var root = AreaTraversal<Day15Cell>.Bfs(area, (0, 0), c => c == Day15Cell.Open);
            var oxygenNode = AreaTraversal<Day15Cell>.FindPosition(MazeGenerator.OxygenSystemPosition, root);
            return AreaTraversal<Day15Cell>.DepthOf(oxygenNode);
        }

        public static long Part2()
        {
            var root = AreaTraversal<Day15Cell>.Bfs(area, MazeGenerator.OxygenSystemPosition, c => c == Day15Cell.Open);
            return AreaTraversal<Day15Cell>.MaxDepth(root)-1;
        }

        static Day15()
        {
            Day15Debug.DebugMode = Day15Debug.Mode.None;
            area = MazeGenerator.GenerateMaze();
        }

        private static Area<Day15Cell> area;
    }

    internal static class MazeGenerator 
    {
        public static Area<Day15Cell> GenerateMaze()
        {
            area = new Area<Day15Cell>(Day15Cell.Unexplored);
            Attempts.Push(Compass.AllDirections);
            new IntCodeComputer(GetData(), Input, Output).Run();
            return area;
        }

        private static long Input()
        {
            var attempt = Attempts.Pop().Where(dir => !Visited.Contains(Compass.PositionAfterMovement(droidPosition, dir))).ToList();
            if (attempt.Count == 0)
            {
                Backtracking = true;
                Attempt = Compass.Opposite(EnteredFrom[droidPosition]);
            }
            else
            {
                Backtracking = false;
                Attempt = attempt[0];
                attempt.RemoveAt(0);
                Attempts.Push(attempt);
            }
            var backTrackingString = Backtracking ? " (BACKTRACKING)" : "";
            Day15Debug.Write($"({droidPosition.x}),{droidPosition.y}) - {Compass.Name(Attempt)}{backTrackingString} : ");
            return Compass.ToLong(Attempt);
        }

        private static bool Output(long status)
        {
            var previousPosition = droidPosition;
            var attemptedPosition = Compass.PositionAfterMovement(droidPosition, Attempt);
            if (status == 0)
            {
                // The repair droid hit a wall. Its position has not changed.
                area.Set(attemptedPosition, Day15Cell.Wall);
                Day15Debug.Set(attemptedPosition, Day15Cell.Wall);
                Day15Debug.WriteLine("BLOCKED");
            }
            else
            {
                // The repair droid has moved one step in the requested direction.
                Day15Debug.WriteLine("OK");
                droidPosition = attemptedPosition;
                if (!Backtracking)
                {
                    area.Set(attemptedPosition, Day15Cell.Open);
                    Day15Debug.Set(attemptedPosition, Day15Cell.Open);
                    if (status == 2)
                    {
                        oxygenSystemPosition = attemptedPosition;
                    }
                    EnteredFrom[attemptedPosition] = Attempt;
                    Visited.Add(attemptedPosition);
                    Attempts.Push(Compass.AllDirections);
                }
            }

            return (Attempts.Count() > 1) || (Attempts.Peek().Count > 0);
        }

        public static (int x, int y) OxygenSystemPosition => oxygenSystemPosition;

        private static Area<Day15Cell> area;
        private static (int x, int y) droidPosition = (0, 0);
        private static (int x, int y) oxygenSystemPosition;
        private static Compass.Direction Attempt;
        private static Stack<List<Compass.Direction>> Attempts = new Stack<List<Compass.Direction>>();
        private static Dictionary<(int x, int y), Compass.Direction> EnteredFrom = new Dictionary<(int x, int y), Compass.Direction>();
        private static HashSet<(int x, int y)> Visited = new HashSet<(int, int)> { droidPosition };
        private static bool Backtracking = false;

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day15Input.txt").ToList();
    }

    public enum Day15Cell { Unexplored, Wall, Open }

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

    internal static class Day15Debug
    {
        public enum Mode { None, Operations, MazeChanges }

        public static Mode DebugMode;

        public static void Set((int x, int y) pos, Day15Cell cell)
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

        private static Dictionary<Day15Cell, ConsoleColor> CellColors = new Dictionary<Day15Cell, ConsoleColor>
        {
            { Day15Cell.Open, ConsoleColor.Yellow },
            { Day15Cell.Unexplored, ConsoleColor.Black },
            { Day15Cell.Wall, ConsoleColor.Red },
        };
    }
}
