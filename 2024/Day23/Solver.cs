using CSharpLib;
using CSharpLib.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day23;

public static class Solver
{
    public static long Part1()
    {
        var n = 0L;
        ReadDataAsAdjacencyList("data.txt", ProcessNewPair);

        void ProcessNewPair(string c1, string c2, Dictionary<string, HashSet<string>> clusters)
        {
            foreach (var c1cluster in clusters[c1])
            {
                foreach (var c2cluster in clusters[c2])
                {
                    if (c1cluster == c2cluster)
                    {
                        if (c1[0] == 't' || c2[0] == 't' || c1cluster[0] == 't')
                        {
                            n++;
                        }
                    }
                }
            }
        }

        return n;
    }

    public static string Part2()
    {
        var neighbors = ReadDataAsAdjacencyList("data.txt");
        var cliques = new BronKerbosch<string>().Solve(neighbors);

        var longestClique = cliques.First();
        foreach(var clique in cliques)
        {
            if (clique.Count > longestClique.Count)
            {
                longestClique = clique;
            }
        }

        return string.Join(",", longestClique.Order());
    }

    private static Dictionary<string, HashSet<string>> ReadDataAsAdjacencyList(string fileName, 
        Action<string, string, Dictionary<string, HashSet<string>>>? processNewPair = null)
    {
        Dictionary<string, HashSet<string>> neighbors = [];
        foreach (var s in new DataLoader(2024, 23).ReadStrings(fileName))
        {
            var c1 = s.Substring(0, 2);
            var c2 = s.Substring(3, 2);
            if (!neighbors.TryGetValue(c1, out var value))
            {
                value = ([]);
                neighbors[c1] = value;
            }
            value.Add(c2);
            if (!neighbors.TryGetValue(c2, out value))
            {
                value = ([]);
                neighbors[c2] = value;
            }
            value.Add(c1);
            processNewPair?.Invoke(c1, c2, neighbors);
        }
        return neighbors;
    }
}