using CSharpLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Y2025.Day10;

public static class Solver
{
    public static long Part1()
    {
        var totalPresses = 0;
        var i = 0;
        var data = LoadData("Data.txt").ToList();
        foreach (var (Target, Presses, _) in data)
        {
            var n = SolvePart1(Target, Presses);
            totalPresses += n;
        }
        return totalPresses;
    }

    public static long Part2()
    {
        return 0;
    }

    private static int SolvePart1(List<bool> target, List<List<int>> presses)
    {
        var state = target.Select(_ => false).ToList();

        var n = 1;
        var x = Step([(state, [])]);
        while(!x.Done)
        {
            n++;
            x = Step(x.Results);
        }

        return n;

        (bool Done, List<(List<bool> State, HashSet<int> UsedPressIndices)> Results) Step(List<(List<bool> State, HashSet<int> UsedPressIndices)> inputs)
        {
            var done = false;
            List<(List<bool> State, HashSet<int> UsedPressIndices)> newStates = [];
            foreach (var input in inputs)
            {
                for (var i = 0; i < presses.Count; i++)
                {
                    if (input.UsedPressIndices.Contains(i))
                    {
                        continue;
                    }
                    var newState = Apply(input.State, presses[i]);
                    newStates.Add((newState, input.UsedPressIndices.Append(i).ToHashSet()));
                    if (StatesAreEqual(newState, target))
                    {
                        done = true;
                    }
                }
            }
            return (done, newStates);
        }
    }

    private static bool StatesAreEqual(List<bool> state1, List<bool> state2)
    {
        for (var i = 0; i < state1.Count; i++)
        {
            if (state1[i] != state2[i])
            {
                return false;
            }
        }
        return true;
    }

    private static List<bool> Apply(List<bool> state, List<int> press)
    {
        var newState = new List<bool>(state);
        foreach (var i in press)
        {
            newState[i] = !newState[i];
        }
        return newState;
    }

    private static IEnumerable<(List<bool> Target, List<List<int>> Presses, List<int> JoltageLevels)> LoadData(string fileName)
    {
        var lines = new DataLoader(2025, 10).ReadStrings(fileName);
        foreach(var line in lines)
        {
            var parts = line.Split(' ');
            var target = parts[0][1..^1].Select(c => c == '#').ToList();
            var presses = parts[1..^1].Select(p => p[1..^1].Split(',').Select(int.Parse).ToList()).ToList();
            var joltageLevels = parts.Last()[1..^1].Split(',').Select(int.Parse).ToList();
            yield return (target, presses, joltageLevels);
        }
    }
}
