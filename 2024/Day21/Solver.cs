using CSharpLib.Algorithms;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;

namespace Y2024.Day21;

// I --(dir)--> R1 --(dir)--> R2 --(dir)--> R3 --(num)--> D

// 029A: <vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A

// 7 8 9
// 4 5 6
// 1 2 3
//   0 A

//   ^ A
// < v >

// I      R1 R1C R2 R2C D  DC
// <vA    v  v   >      A
// <A     <  <   v      A
// A      <  <   <
// >>^A   A  A   <  <   0
// vA     >  >   v      0
// A      >  >   >      0
// <^A    ^  ^   A      0
// >A     A  A   A  A   0  0
// <v<A   <  <   ^      0
// >>^A   A  A   ^  ^   2  
// vA     >  >   A      2    
// ^A     A  A   A  A   2  2
// <vA    v  v   >      2
// >^A    A  A   >  >   3
// <v<A   <  <   v      3
// >^A    ^  ^   ^      3
// >A     A  A   ^  ^   6
// A      A  A   ^  ^   9
// vA     >  >   A      9
// ^A     A  A   A  A   9  9
// <v<A   <  <   ^      9
// >A     v  v   v      9
// >^A    A  A   v  v   6
// A      A  A   v  v   3
// A      A  A   v  v   A
// vA     >  >   >      A
// <^A    ^  ^   A      A
// >A     A  A   A  A   A  A 

// State: R1, R2, D, emitted
// neighbors

public static class Solver
{
    public static long Part1()
    {
        return Solve(2, "463A") + Solve(2, "340A") + Solve(2, "129A") + Solve(2, "083A") + Solve(2, "341A");
    }

    public static long Part2()
    {
        return Solve(25, "463A") + Solve(25, "340A") + Solve(25, "129A") + Solve(25, "083A") + Solve(25, "341A"); // 68736 is too low
    }

    private static long Solve(int noOfIntermediateRobots, string target)
    {
        Console.Write($"Solving for {noOfIntermediateRobots} - {target}: ");
        var solution = Dijkstra<(string Robots, char D, string Code)>.Solve((new string('A', noOfIntermediateRobots), 'A', ""), doGetNeighbors, isTarget);
        var result = long.Parse(target.Replace("A", "")) * solution.TotalCost;
        Console.WriteLine(result);
        return result;

        IEnumerable<((string Robots, char D, string Code), long)> doGetNeighbors((string Robots, char D, string Code) state)
        {
            foreach(var neighbor in getNeighbors(state))
            {
                //Console.WriteLine($"{neighbor.Item1.Robots},{neighbor.Item1.D}, {neighbor.Item1.Code}");
                yield return neighbor;
            }
        }

        IEnumerable<((string Robots, char D, string Code), long)> getNeighbors((string Robots, char D, string Code) state)
        {
            if (target.StartsWith(state.Code)) // Or we have already entered an incorrect code, no neighbors then...
            {
                // If I press one of the directional buttons, the first robot's arm moves one step but nothing else happens...
                foreach (var n in DirNeighbors[state.Robots[0]])
                {
                    yield return ((state.Robots.WithReplacedChar(0, n), state.D, state.Code), 1);
                }

                // ... or I can press "A".
                var indexOfFirstNonA = state.Robots.FindIndex(c => c != 'A');

                // If all robots are positioned on "A", we add the current numeric position to the output.
                if (indexOfFirstNonA == -1)
                {
                    yield return ((state.Robots, state.D, state.Code + state.D), 1);
                    //Console.WriteLine(state.Code + state.D);
                }

                // If all but the last robot are positioned on "A", we update the position on the numeric keyboard according to the last robot.
                else if (indexOfFirstNonA == state.Robots.Length - 1)
                {
                    var lastRobot = state.Robots[indexOfFirstNonA];
                    if ((lastRobot == '>' && NumRight.TryGetValue(state.D, out var d)) ||
                        (lastRobot == 'v' && NumDown.TryGetValue(state.D, out d)) ||
                        (lastRobot == '<' && NumLeft.TryGetValue(state.D, out d)) ||
                        (lastRobot == '^' && NumUp.TryGetValue(state.D, out d)))
                        yield return ((state.Robots, d, state.Code), 1);
                }

                // Otherwise, we look at the first "non-A" and update the one after it.
                else
                {
                    var robot = state.Robots[indexOfFirstNonA];
                    var nextRobot = state.Robots[indexOfFirstNonA + 1];
                    if ((robot == '>' && DirRight.TryGetValue(nextRobot, out var r)) ||
                        (robot == 'v' && DirDown.TryGetValue(nextRobot, out r)) ||
                        (robot == '<' && DirLeft.TryGetValue(nextRobot, out r)) ||
                        (robot == '^' && DirUp.TryGetValue(nextRobot, out r)))
                    {
                        yield return ((state.Robots.WithReplacedChar(indexOfFirstNonA + 1, r), state.D, state.Code), 1);
                    }
                }

            }
        }

        bool isTarget((string Robots, char D, string Code) state)
        {
            return state.Code == target;
        }
    }

    static Dictionary<char, char> NumUp = new()
    {
        { '4', '7' },
        { '5', '8' },
        { '6', '9' },
        { '1', '4' },
        { '2', '5' },
        { '3', '6' },
        { '0', '2' },
        { 'A', '3' }
    };

    static Dictionary<char, char> NumLeft = new()
    {
        { '8', '7' },
        { '9', '8' },
        { '5', '4' },
        { '6', '5' },
        { '2', '1' },
        { '3', '2' },
        { 'A', '0' }
    };

    static Dictionary<char, char> NumDown = new()
    {
        { '7', '4' },
        { '8', '5' },
        { '9', '6' },
        { '4', '1' },
        { '5', '2' },
        { '6', '3' },
        { '2', '0' },
        { '3', 'A' }
    };

    static Dictionary<char, char> NumRight = new()
    {
        { '7', '8' },
        { '8', '9' },
        { '4', '5' },
        { '5', '6' },
        { '1', '2' },
        { '2', '3' },
        { '0', 'A' }
    };

    static Dictionary<char, char> DirUp = new()
    {
        { 'v', '^' },
        { '>', 'A' }
    };

    static Dictionary<char, char> DirLeft = new()
    {
        { 'A', '^' },
        { 'v', '<' },
        { '>', 'v' }
    };

    static Dictionary<char, char> DirDown = new()
    {
        { '^', 'v' },
        { 'A', '>' }
    };

    static Dictionary<char, char> DirRight = new()
    {
        { '^', 'A' },
        { '<', 'v' },
        { 'v', '>' }
    };

    static readonly Dictionary<char, List<char>> DirNeighbors = new()
    {
        { '^', ['A', 'v'] },
        { 'A', ['^', '>'] },
        { '<', ['v'] },
        { 'v', ['<', '^', '>'] },
        { '>', ['A', 'v'] }
    };
}
