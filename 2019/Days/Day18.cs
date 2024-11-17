using AdventOfCode.Common;
using CSharpLib.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2019
{
    public static class Day18
    {
        public static long Part1()
        {
            LoadData();
            var initialState = new State(doors, keys, pos);
            var result = Dijkstra<State>.Solve(initialState, CalculatePossibleStates, s => s.Keys.Count == 0, PrettyPrint);
            return result.TotalCost;
        }

        public static long Part2()
        {
            return 0;
        }

        private static IEnumerable<Edge<State>> CalculatePossibleStates(State state)
        {
            if (TryGetPossibleState(-1, 0, out var s)) { yield return new(state, s, 1); }
            if (TryGetPossibleState(0, -1, out s)) { yield return new(state, s, 1); }
            if (TryGetPossibleState(0, 1, out s)) { yield return new(state, s, 1); }
            if (TryGetPossibleState(1, 0, out s)) { yield return new(state, s, 1); }

            bool TryGetPossibleState(int rowOffset, int colOffset, out State newState)
            {
                var row = state.Pos.Row + rowOffset;
                var col = state.Pos.Col + colOffset;
                var isDoor = state.Doors.TryGetValue((row, col), out _);
                if (row < 0 || row > rows-1 || col < 0 || col > cols-1 || isDoor || walls.Contains((row, col)))
                {
                    newState = state;
                    return false;
                }

                var newKeys = new Dictionary<(int Row, int Col), char>(state.Keys);
                var newDoors = new Dictionary<(int Row, int Col), char>(state.Doors);
                var isKey = newKeys.TryGetValue((row, col), out var key);
                if (isKey)
                {
                    newKeys.Remove((row, col));
                    var door = newDoors.FirstOrDefault(kvp => kvp.Value == char.ToUpper(key));
                    if (door.Value == char.ToUpper(key))
                    {
                        newDoors.Remove(door.Key);
                    }
                }

                newState = new(newDoors, newKeys, (row, col));
                return true;
            }
        }

        private static void LoadData()
        {
            var row = 0;
            foreach (var line in DataReader.ReadStrings("Day18Input.txt").ToList())
            {
                cols = line.Length;
                var col = 0;
                foreach (var c in line.ToCharArray())
                {
                    if (c == '@')
                    {
                        pos = (row, col);
                    }
                    else if (c == '#')
                    {
                        walls.Add((row, col));
                    }
                    else if (char.IsUpper(c))
                    {
                        doors[(row, col)] = c;
                    }
                    else if (char.IsLower(c))
                    {
                        keys[(row, col)] = c;
                    }
                    col++;
                }
                row++;
            }
            rows = row;
        }

        private static string PrettyPrint(State state)
        {
            var doorsInOrder = state.Doors.Select(kvp => $"({kvp.Key.Row},{kvp.Key.Col})={kvp.Value}").ToList();
            doorsInOrder.Sort();

            var keysInOrder = state.Keys.Select(kvp => $"({kvp.Key.Row},{kvp.Key.Col})={kvp.Value}").ToList();
            keysInOrder.Sort();

            var keys = '[' + string.Join(',', keysInOrder) + ']';
            var doors = '[' + string.Join(',', doorsInOrder) + ']';
            
            return $"{state.Pos} {keys} {doors}";
        }

        private static void PrintResult(State initialState, Solution<State> result)
        {
            Console.WriteLine(PrettyPrint(initialState));
            foreach (var s in result.Path)
            {
                Console.WriteLine(PrettyPrint(s.Destination));
            }
        }

        // Fixed, will not change
        private static HashSet<(int, int)> walls = [];
        private static int cols;
        private static int rows;

        // Will change, must be part of state
        private static Dictionary<(int Row, int Col), char> doors = [];
        private static Dictionary<(int Row, int Col), char> keys = [];
        private static (int Row, int Col) pos;

        record State(Dictionary<(int Row, int Col), char> Doors, Dictionary<(int Row, int Col), char> Keys, (int Row, int Col) Pos);
    }
}
