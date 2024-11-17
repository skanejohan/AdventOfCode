using Priority_Queue;
using System;
using System.Collections.Generic;

namespace CSharpLib.Algorithms;

public static class Dijkstra<T> where T : notnull, IEquatable<T>
{
    /// <summary>
    /// Solves a shortest-path graph problem using Dijkstra's algorithm.
    /// </summary>
    /// <param name="start">This is the start (initial) state.</param>
    /// <param name="calculateEdgesToNeighbors">Given a state, it is this function's job to calculate the edges to all adjacent states.</param>
    /// <param name="isTarget">Given a state, this function should tell if the state is the target state or not.</param>
    /// <param name="getIdentifier">If left out, the state itself will be placed in a hash set indicating which states that have been seen.
    /// You may specify a function that returns an object (e.g. a string) uniquely representing a state. This may be needed if the standard
    /// equality comparison does not work (and we fail to detect states that we have already seen).</param>
    /// <returns>An object indicating all edges, and the total cost.</returns>
    /// <exception cref="Exception">Thrown if no path was found.</exception>
    public static Solution<T> Solve(T start, Func<T, IEnumerable<Edge<T>>> calculateEdgesToNeighbors, Func<T, bool> isTarget, Func<T, object>? getIdentifier = null)
    {
        Func<T, object> id = getIdentifier ?? (t => t);

        var visitedNodes = new HashSet<object>();
        var nodesToVisit = new SimplePriorityQueue<T, long>();
        var lowestCostToDestinationNode = new Dictionary<T, long>() 
        { 
            [start] = 0 
        };
        var lowestCostEdgeToDestinationNode = new Dictionary<T, Edge<T>>
        {
            [start] = new(start, start, 0)
        };

        nodesToVisit.Enqueue(start, 0);
        while (nodesToVisit.Count > 0)
        {
            var currentNode = nodesToVisit.Dequeue();

            if (visitedNodes.Contains(id(currentNode)))
            {
                continue;
            }

            var currentCost = lowestCostToDestinationNode[currentNode];

            visitedNodes.Add(id(currentNode));

            if (isTarget(currentNode))
            {

                return new(GetPath(start, currentNode, lowestCostEdgeToDestinationNode), currentCost);
            }

            foreach (var edge in calculateEdgesToNeighbors(currentNode))
            {
                var totalCost = currentCost + edge.Cost;
                if (totalCost < lowestCostToDestinationNode.GetValueOrDefault(edge.Destination, long.MaxValue))
                {
                    lowestCostToDestinationNode[edge.Destination] = totalCost;
                    lowestCostEdgeToDestinationNode[edge.Destination] = new(currentNode, edge.Destination, currentCost);
                    nodesToVisit.Enqueue(edge.Destination, totalCost);
                }
            }
        }

        throw new Exception("No path found");
    }

    private static List<Edge<T>> GetPath(T source, T destination, Dictionary<T, Edge<T>> lowestCostEdgeToDestination)
    {
        var cell = destination;
        var items = new List<Edge<T>>();
        do
        {
            items.Add(lowestCostEdgeToDestination[cell]);
            cell = lowestCostEdgeToDestination[cell].Source;
        } while (!cell.Equals(source));
        items.Reverse();
        return items;
    }
}

public record Edge<T>(T Source, T Destination, long Cost);
public record Solution<T>(List<Edge<T>> Path, long TotalCost);
