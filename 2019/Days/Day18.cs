using AdventOfCode.Common;
using CSharpLib.Algorithms;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2019;

public static class Day18
{
    public static long Part1()
    {
        var robot = LoadData();
        CalculateAllPaths(robot);
        return Dijkstra<((int, int), int)>.Solve((robot, 0), GetNeighbors, x => x.Item2 == Math.Pow(2, highestKeyIndex + 1) - 1).TotalCost;

        static IEnumerable<(((int, int), int), long)> GetNeighbors(((int Row, int Col) Pos, int Keys) current)
        {
            foreach(var kvp in allPaths[current.Pos])
            {
                var target = kvp.Key;
                foreach(var (Cost, RequiredKeys) in kvp.Value)
                {
                    if (WeHaveAllRequiredKeys(current.Keys, RequiredKeys))
                    {
                        var keyIndex = keyIndexByPosition[target];
                        var keys = current.Keys.WithBitSet(keyIndex);
                        yield return ((target, keys), Cost);
                    }
                }
            }

            static bool WeHaveAllRequiredKeys(int carried, int required)
            {
                return (required & ~carried) == 0;
            }
        }
    }

    public static long Part2()
    {
        return 0;
    }

    /// <summary>
    /// Populate cells, keys, doors, highestKeyIndex and doorIndexByPosition, and return the robot's position
    /// </summary>
    private static (int Row, int Col) LoadData()
    {
        cells = [];
        keys = new (int, int)[MAX_KEYS];
        doors = new (int, int)[MAX_KEYS];
        doorIndexByPosition = [];
        keyIndexByPosition = [];
        highestKeyIndex = 0;
        var robot = (0, 0);

        var row = 0;
        foreach (var line in DataReader.ReadStrings("Day18Input.txt").ToList())
        {
            var col = 0;
            foreach (var c in line.ToCharArray())
            {
                var pos = (row, col);
                if (c == '#')
                {
                }
                else if (c == '@')
                {
                    robot = pos;
                    cells.Add(pos);
                }
                else if (c == '.')
                {
                    cells.Add(pos);
                }
                else if (char.IsUpper(c))
                {
                    var doorIndex = c - 'A';
                    doors[doorIndex] = pos;
                    doorIndexByPosition[pos] = doorIndex;
                    cells.Add(pos);
                }
                else if (char.IsLower(c))
                {
                    var keyIndex = c - 'a';
                    keys[keyIndex] = pos;
                    keyIndexByPosition[pos] = keyIndex;
                    highestKeyIndex = Math.Max(highestKeyIndex, keyIndex);
                    cells.Add(pos);
                }
                col++;
            }
            row++;
        }
        return robot;
    }

    /// <summary>
    /// Populate allPaths.
    /// </summary>
    private static void CalculateAllPaths(params (int, int)[] robots)
    {
        allPaths = [];

        var robotsAndKeys = new List<(int, int)>(robots);
        for (var i = 0; i <= highestKeyIndex; i++)
        {
            robotsAndKeys.Add(keys[i]);
        }

        for (var i = 0; i < robotsAndKeys.Count-1; i++)
        {
            var pos1 = robotsAndKeys[i];
            for (var j = i + 1; j < robotsAndKeys.Count; j++)
            {
                var pos2 = robotsAndKeys[j];
                var paths = PathFinder.FindAllPaths(pos1, pos2, GetNeighbors);

                if (paths.Count > 0)
                {
                    foreach (var path in paths)
                    {
                        var requiredKeys = 0;
                        foreach (var pos in path)
                        {
                            if (doorIndexByPosition.TryGetValue(pos, out var doorIndex))
                            {
                                requiredKeys = requiredKeys.WithBitSet(doorIndex); // Door and key index are matching
                            }
                        }
                        AddToAllPaths(pos1, pos2, (path.Count - 1, requiredKeys));
                        if (pos1 != robots[0]) // TODO or any other robot
                        {
                            AddToAllPaths(pos2, pos1, (path.Count - 1, requiredKeys));
                        }
                    }
                }
            }
        }

        static IEnumerable<(int Row, int Col)> GetNeighbors((int R, int C) p)
        {
            foreach (var pos in new[] { (p.R, p.C - 1), (p.R, p.C + 1), (p.R - 1, p.C), (p.R + 1, p.C) })
            {
                if (cells.Contains(pos))
                {
                    yield return pos;
                }
            }
        }

        static void AddToAllPaths((int, int) pos1, (int, int) pos2, (int, int) data)
        {
            if (!allPaths.ContainsKey(pos1))
            {
                allPaths[pos1] = [];
            }
            if (!allPaths[pos1].ContainsKey(pos2))
            {
                allPaths[pos1][pos2] = [];
            }
            allPaths[pos1][pos2].Add(data);
        }

        //static void PrepareAllPathsFor((int, int) key1, (int,  int) key2)
        //{
        //    if (!allPaths.ContainsKey(key1))
        //    {
        //        allPaths[key1] = [];
        //    }
        //    if (!allPaths[key1].ContainsKey(key2))
        //    {
        //        allPaths[key1][key2] = [];
        //    }
        //    if (!allPaths.ContainsKey(key2))
        //    {
        //        allPaths[key2] = [];
        //    }
        //    if (!allPaths[key2].ContainsKey(key1))
        //    {
        //        allPaths[key2][key1] = [];
        //    }
        //}
    }

    private static void PrintAllPaths() // For debugging.
    {
        foreach (var kvp1 in allPaths)
        {
            foreach (var kvp2 in allPaths[kvp1.Key])
            {
                Console.WriteLine($"{kvp1.Key} - {kvp2.Key}");
                foreach (var (cost, requiredKeys) in kvp2.Value)
                {
                    Console.WriteLine($"  Cost: {cost}. Required keys: {requiredKeys}");
                }
            }
        }
    }

    // All cells that are not walls, i.e. also '@', keys and doors.
    private static HashSet<(int Row, int Col)> cells;

    // All cells containing keys. Indexed so that the position of key 'a' is at index 0, etc.
    private static (int Row, int Col)[] keys;

    // The highest used index in keys.
    private static int highestKeyIndex;

    // For quick lookup - given a position, which key, if any, is there?
    private static Dictionary<(int Row, int Col), int> keyIndexByPosition;

    // All cells containing doors. Indexed so that the position of door 'A' is at index 0, etc.
    private static (int Row, int Col)[] doors;

    // For quick lookup - given a position, which door, if any, is there?
    private static Dictionary<(int Row, int Col), int> doorIndexByPosition;

    // All paths from a given key to all other keys.
    private static Dictionary<
        (int Row, int Col), // Source key
        Dictionary<
            (int Row, int Col), // Target key
            List<(int Cost, int RequiredKeys)>>> allPaths; // A list of cost and required keys for each possible path

    // Allow for at most 32 doors and keys.
    private const int MAX_KEYS = 32;
}
