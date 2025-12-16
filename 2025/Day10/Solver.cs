using CSharpLib;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day10;

public static class Solver
{
    public static long Part1()
    {
        var totalPresses = 0;
        var data = LoadData("Data.txt").ToList();
        foreach (var (Indicators, Buttons, _) in data)
        {
            var buttonPresses = GetButtonPressesNeededForPartOne(Indicators, Buttons);
            totalPresses += buttonPresses.Select(b => b.CountBits()).Min();
        }
        return totalPresses;
    }

    public static long Part2()
    {
        return 0;
    }

    private static IEnumerable<int> GetButtonPressesNeededForPartOne(Indicators targetIndicators, List<Button> buttons)
    {
        int targetInds = targetIndicators.AsInt();
        var btns = buttons.Select(b => b.AsInt()).ToList();

        // Indicators and buttons are represented as int. For indicator: bit n set means light is lit.
        // For button: bit n set means button affects indicator light n. Since pressing a button twice
        // is the same as not pressing it at all, we only need to test with each button pressed or not.
        // So by looking at the bits in buttonsCounter, we see which buttons were pressed. 
        for (var buttonsCounter = 0; buttonsCounter < Math.Pow(2, buttons.Count); buttonsCounter++)
        {
            var indicators = 0;
            for (var i = 0; i < buttons.Count; i++)
            {
                var mask = 1 << i;
                if ((buttonsCounter & mask) == mask)
                {
                    indicators = indicators ^ btns[i];
                    if (indicators == targetInds)
                    {
                        yield return buttonsCounter;
                    }
                }
            }
        }
    }

    private static IEnumerable<(Indicators Indicators, List<Button> Buttons, List<int> JoltageLevels)> LoadData(string fileName)
    {
        var lines = new DataLoader(2025, 10).ReadStrings(fileName);
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var target = new Indicators(parts[0][1..^1].Select(c => c == '#'));
            var presses = parts[1..^1].Select(p => p[1..^1].Split(',').Select(int.Parse).ToList()).Select(l => new Button(l)).ToList();
            var joltageLevels = parts.Last()[1..^1].Split(',').Select(int.Parse).ToList();
            yield return (target, presses, joltageLevels);
        }
    }
}
