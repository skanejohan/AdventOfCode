using Priority_Queue;
using System;
using System.Collections.Generic;

namespace CSharpLib.Algorithms
{
    public class Dijkstra<T> where T : notnull, IEquatable<T>
    {
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
            edges.Clear();
            edges[start] = (start, 0);
            var visited = new HashSet<T>();
            var queue = new SimplePriorityQueue<T, long>();
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
                    if (current.Equals(target))
                    {
                        break;
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
        private T start, target;
        private long cost;
    }
}
