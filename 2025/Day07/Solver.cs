using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day07;

public static class Solver
{
    public static long Part1()
    {
        var (tachyonBeamPositions, splitterPositionsList) = LoadData("Data.txt");

        var totalSplits = 0;
        foreach(var splitterPositions in splitterPositionsList)
        {
            var (NoOfSplits, NewBeams) = SplitTachyonBeams(tachyonBeamPositions, splitterPositions);
            tachyonBeamPositions = NewBeams;
            totalSplits += NoOfSplits;
        }
        return totalSplits;

        static (int NoOfSplits, List<int> NewBeams) SplitTachyonBeams(List<int> tachyonBeamPositions, List<int> splitterPositions)
        {
            var result = (0, new List<int>());
            foreach (var tachyonBeamPosition in tachyonBeamPositions)
            {
                if (splitterPositions.Contains(tachyonBeamPosition))
                {
                    result.Item2.Add(tachyonBeamPosition - 1);
                    result.Item2.Add(tachyonBeamPosition + 1);
                    result.Item1++;
                }
                else
                {
                    result.Item2.Add(tachyonBeamPosition);
                }
            }
            return (result.Item1, [.. result.Item2.Distinct()]);
        }
    }

    public static long Part2()
    {
        var data = new DataLoader(2025, 7).ReadStrings("Data.txt").ToList();
        var ways = data[0].Select(c => c == 'S' ? 1L : 0L).ToList();
        foreach(var line in data.Skip(1))
        {
            ways = Step(ways, line);
        }
        return ways.Sum();

        static List<long> Step(List<long> currentWays, string line)
        {
            var ways = new List<long>();
            for (var i = 0; i < currentWays.Count; i++)
            {
                var n = line[i] == '^' ? 0 : currentWays[i];
                if (i > 0 && line[i - 1] == '^')
                {
                    n += currentWays[i - 1];
                }
                if (i < currentWays.Count - 1 && line[i + 1] == '^')
                {
                    n += currentWays[i + 1];
                }
                ways.Add(n);
            }
            return ways;
        }
    }

    private static (List<int> TachyonBeamPositions, List<List<int>> SplitterPositions) LoadData(string fileName)
    {
        var data = new DataLoader(2025, 7).ReadStrings(fileName);
        List<int> tachyonBeamPositions = [data.First().IndexOf('S')];
        List<List<int>> splitterPositionsList = [.. data.Select(line => GetSplitterPositions(line).ToList())];
        return (tachyonBeamPositions, splitterPositionsList);

        static IEnumerable<int> GetSplitterPositions(string data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] == '^')
                {
                    yield return i;
                }
            }
        }
    }
}
