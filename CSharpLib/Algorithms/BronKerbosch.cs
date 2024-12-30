using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.Algorithms
{
    // Sample usage: 
    //
    //    Dictionary<string, HashSet<string>> neighbors = new()
    //    {
    //        { "a", new HashSet<string>{ "b", "c" } },
    //        { "b", new HashSet<string>{ "a", "c" } },
    //        { "c", new HashSet<string>{ "a", "b" } },
    //        { "d", new HashSet<string>{ "e", "f" } },
    //        { "e", new HashSet<string>{ "d", "f" } },
    //        { "f", new HashSet<string>{ "d", "e" } },
    //    };
    //    var cliques = new BronKerbosch<string>().Solve(neighbors);

    // Sample usage: 
    //
    //    Dictionary<string, HashSet<string>> neighbors = new()
    //    {
    //        { "1", new HashSet<string>{ "5", "2" } },
    //        { "2", new HashSet<string>{ "1", "3", "5" } },
    //        { "3", new HashSet<string>{ "2", "4" } },
    //        { "4", new HashSet<string>{ "3", "5", "6" } },
    //        { "5", new HashSet<string>{ "1", "2", "4" } },
    //        { "6", new HashSet<string>{ "4" } },
    //    };
    //    var cliques = new BronKerbosch<string>().Solve(neighbors);

    public class BronKerbosch<T> where T : notnull
    {
        /// <summary>
        /// Returns all cliques in an undirected graph. The graph should be specified as an adjacency list.
        /// </summary>
        public List<HashSet<T>> Solve(Dictionary<T, HashSet<T>> adjacencyList)
        {
            cliques = [];
            this.adjacencyList = adjacencyList;
            SolveRec([], [.. adjacencyList.Keys], []);
            return cliques;
        }

        private void SolveRec(HashSet<T> currentClique, HashSet<T> potentialCandidates, HashSet<T> excluded)
        {
            if (potentialCandidates.Count == 0 && excluded.Count == 0)
            {
                if (currentClique.Count > 2)
                {
                    cliques.Add(currentClique);
                }
                return;
            }

            // Select a pivot vertex from P ∪ X with the maximum degree
            var pivotVertex = GetMaxDegreeVertexFrom(potentialCandidates.Union(excluded).ToHashSet());

            // Candidates are vertices in P that are not neighbors of the pivot
            var candidates = new HashSet<T>(potentialCandidates).Except(adjacencyList[pivotVertex]);

            foreach (var v in candidates)
            {
                // New clique including vertex v
                var newClique = currentClique.Union([v]).ToHashSet();

                // New potential candidates are neighbors of v in P
                var newPotentialCandidates = potentialCandidates.Intersect(adjacencyList[v]).ToHashSet();

                // New excluded vertices are neighbors of v in X
                var newExcluded = excluded.Intersect(adjacencyList[v]).ToHashSet();

                // Recursive call with updated sets
                SolveRec(newClique, newPotentialCandidates, newExcluded);

                // Move vertex v from P to X
                potentialCandidates.Remove(v);
                excluded.Add(v);
            }
        }

        private T GetMaxDegreeVertexFrom(HashSet<T> vertices)
        {
            var currentSize = 0;
            var maxDegreeVertex = vertices.First();
            foreach(var v in vertices)
            {
                if (adjacencyList[v].Count > currentSize)
                {
                    currentSize = adjacencyList[v].Count;
                    maxDegreeVertex = v;
                }
            }
            return maxDegreeVertex;
        }

        private Dictionary<T, HashSet<T>> adjacencyList = [];
        private List<HashSet<T>> cliques = [];
    }
}
