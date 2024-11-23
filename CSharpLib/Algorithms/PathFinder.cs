using System;
using System.Collections.Generic;

namespace CSharpLib.Algorithms;

public static class PathFinder
{

    /// <summary>
    /// Find all paths from "from" to "to".
    /// </summary>
    public static List<List<T>> FindAllPaths<T>(T from, T to, Func<T, IEnumerable<T>> getNeighbors) where T : IEquatable<T>
    {
        HashSet<T> visited = [];
        List<List<T>> paths = [];
        List<T> path = [from];
        FindAllPathsRec(from);
        return paths;

        void FindAllPathsRec(T current)
        {
            if (current.Equals(to))
            {
                paths.Add(new List<T>(path));
                return;
            }
            visited.Add(current);
            foreach (var n in getNeighbors(current))
            {
                if (!visited.Contains(n))
                {
                    path.Add(n);
                    FindAllPathsRec(n);
                    path.RemoveAt(path.Count - 1);
                }
            }
            visited.Remove(current);
        }
    }
}
