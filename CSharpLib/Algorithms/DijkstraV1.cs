using System;
using System.Collections.Generic;

namespace CSharpLib.Algorithms
{
    public class DijkstraV1<T> where T : notnull, IEquatable<T>
    {
        public DijkstraV1(Func<T, bool> targetIsReached = null)
        {
            this.targetIsReached = targetIsReached;
        }

        public IEnumerable<(T T, long Distance)> ShortestPath()
        {
            var cell = target;
            var items = new List<(T, long)>
            {
                (target, cost)
            };
            do
            {
                items.Add(edges[cell]);
                cell = edges[cell].Item1;
            } while (!cell.Equals(start));
            items.Reverse();
            return items;
        }

        public long Solve(T start, T target, Func<T, IEnumerable<(T, long)>> transformer)
        {
            if (targetIsReached == null)
            {
                targetIsReached = c => c.Equals(target);
            }
            edges.Clear();
            edges[start] = (start, 0);
            var visited = new HashSet<T>();
            var queue = new PriorityQueue<T, long>();
            var dist = new Dictionary<T, long>() { [start] = 0 };
            this.target = target;
            this.start = start;

            queue.Enqueue(start, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!visited.Contains(current))
                {
                    var currentCost = dist[current];

                    visited.Add(current);
                    if (targetIsReached(current))
                    {
                        return currentCost;
                    }

                    foreach (var (next, cost) in transformer(current))
                    {
                        var totalCost = currentCost + cost;
                        if (totalCost < dist.GetValueOrDefault(next, long.MaxValue))
                        {
                            dist[next] = totalCost;
                            edges[next] = (current, currentCost);
                            queue.Enqueue(next, totalCost);
                        }
                    }
                }
            }

            cost = dist[target];
            return cost;
        }

        private readonly Dictionary<T, (T, long)> edges = new Dictionary<T, (T, long)>();
        private Func<T, bool> targetIsReached;
        private T start, target;
        private long cost;
    }
}
