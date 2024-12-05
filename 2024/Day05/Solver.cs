using CSharpLib;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day05;

public static class Solver
{
    public static long Part1()
    {
        LoadData("Data.txt");
        return updates
            .Where(UpdateOk)
            .Select(SelectMiddlePage)
            .Sum();
    }

    public static long Part2()
    {
        LoadData("Data.txt");
        return updates
            .Where(l => !UpdateOk(l))
            .Select(u => u.Order(new PageComparer()).ToList())
            .Select(SelectMiddlePage)
            .Sum();

    }

    private static void LoadData(string fileName)
    {
        var data = new DataLoader(2024, 5).ReadStrings(fileName).ChunkBy(s => s.Trim() == "");
        var dRules = data.First();
        var dUpdates = data.Skip(1).First();

        rules = [];
        foreach(var rule in dRules)
        {
            var parts = rule.Split("|");
            var key = int.Parse(parts[0]);
            var val = int.Parse(parts[1]);
            if (!rules.TryGetValue(key, out var dict))
            {
                dict = [];
                rules[key] = dict;
            }
            dict.Add(val);
        }
        updates = dUpdates.Select(p => p.Split(",").Select(int.Parse).ToList());
    }

    private static bool UpdateOk(List<int> pages)
    {
        for (var i = 0; i < pages.Count-1; i++)
        {
            for (var j = i+1; j < pages.Count; j++)
            {
                if (!rules.TryGetValue(pages[i], out var dict) || !dict.Contains(pages[j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static int SelectMiddlePage(List<int> pages)
    {
        return pages[pages.Count / 2];
    }

    class PageComparer : IComparer<int>
    {
        public int Compare(int a, int b)
        {
            if (rules.TryGetValue(a, out var dict) && dict.Contains(b))
            {
                return -1;
            }
            if (rules.TryGetValue(b, out dict) && dict.Contains(a))
            {
                return 1;
            }
            return 0;
        }
    }

    private static Dictionary<int, HashSet<int>> rules = [];
    private static IEnumerable<List<int>> updates = [];
}
