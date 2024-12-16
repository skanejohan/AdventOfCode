using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.Algorithms;

public static class Dijkstra<T> where T : notnull, IEquatable<T>
{

    public static Solution<T> SolveEx(T start, Func<T, IEnumerable<(T, long)>> calculateEdgesToNeighbors, Func<T, List<(T, long)>, long, bool> isTarget)
    {
        var visitedNodes = new HashSet<object>();
        var nodesToVisit = new PriorityQueue<T, long>();
        var lowestCostToDestinationNode = new Dictionary<T, long>()
        {
            [start] = 0
        };
        var lowestCostEdgeToDestinationNode = new Dictionary<T, (T, long)>
        {
            [start] = (start, 0)
        };

        nodesToVisit.Enqueue(start, 0);
        while (nodesToVisit.Count > 0)
        {
            var currentNode = nodesToVisit.Dequeue();

            if (visitedNodes.Contains(currentNode))
            {
                continue;
            }

            var currentCost = lowestCostToDestinationNode[currentNode];

            visitedNodes.Add(currentNode);

            var pathSoFar = GetPath(start, currentNode, lowestCostEdgeToDestinationNode);
            if (isTarget(currentNode, pathSoFar, currentCost))
            {
                return new(pathSoFar, currentCost);
            }

            foreach (var (destination, cost) in calculateEdgesToNeighbors(currentNode))
            {
                var totalCost = currentCost + cost;
                if (totalCost < lowestCostToDestinationNode.GetValueOrDefault(destination, long.MaxValue))
                {
                    lowestCostToDestinationNode[destination] = totalCost;
                    lowestCostEdgeToDestinationNode[destination] = (currentNode, currentCost);
                    nodesToVisit.Enqueue(destination, totalCost);
                }
            }
        }

        throw new Exception("No path found");
    }

    /// <summary>
    /// Solves a shortest-path graph problem using Dijkstra's algorithm.
    /// </summary>
    /// <param name="start">This is the start (initial) state.</param>
    /// <param name="calculateEdgesToNeighbors">Given a state, it is this function's job to calculate all adjacent states and costs to get there.</param>
    /// <param name="isTarget">Given a state, this function should tell if the state is the target state or not.</param>
    /// <exception cref="Exception">Thrown if no path was found.</exception>
    public static Solution<T> Solve(T start, Func<T, IEnumerable<(T, long)>> calculateEdgesToNeighbors, Func<T, bool> isTarget)
    {
        var visitedNodes = new HashSet<object>();
        var nodesToVisit = new PriorityQueue<T, long>();
        var lowestCostToDestinationNode = new Dictionary<T, long>() 
        { 
            [start] = 0 
        };
        var lowestCostEdgeToDestinationNode = new Dictionary<T, (T, long)>
        {
            [start] = (start, 0)
        };

        nodesToVisit.Enqueue(start, 0);
        while (nodesToVisit.Count > 0)
        {
            var currentNode = nodesToVisit.Dequeue();

            if (visitedNodes.Contains(currentNode))
            {
                continue;
            }

            var currentCost = lowestCostToDestinationNode[currentNode];

            visitedNodes.Add(currentNode);

            if (isTarget(currentNode))
            {
                return new(GetPath(start, currentNode, lowestCostEdgeToDestinationNode), currentCost);
            }

            foreach (var (destination, cost) in calculateEdgesToNeighbors(currentNode))
            {
                var totalCost = currentCost + cost;
                if (totalCost < lowestCostToDestinationNode.GetValueOrDefault(destination, long.MaxValue))
                {
                    lowestCostToDestinationNode[destination] = totalCost;
                    lowestCostEdgeToDestinationNode[destination] = (currentNode, currentCost);
                    nodesToVisit.Enqueue(destination, totalCost);
                }
            }
        }

        throw new Exception("No path found");
    }

    private static List<(T, long)> GetPath(T source, T destination, Dictionary<T, (T, long)> lowestCostEdgeToDestination)
    {
        var cell = destination;
        var items = new List<(T, long)>();
        do
        {
            items.Add(lowestCostEdgeToDestination[cell]);
            cell = lowestCostEdgeToDestination[cell].Item1;
        } while (!cell.Equals(source));
        items.Reverse();
        return items;
    }
}

public record Solution<T>(List<(T, long)> Path, long TotalCost);
