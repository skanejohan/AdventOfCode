namespace Lib.Utils

open Dijkstra.NET.Graph
open Dijkstra.NET.ShortestPath

module Dijkstra =

    // Calculate the shortest path from src to dst among the given nodes and weighted edges. 
    let dijkstra (nodes : uint32 seq) edges src dst = 
        let g = new Graph<int, string>()
        for n in nodes do
            g.AddNode(int n) |> ignore
        for (n1, n2, w) in edges do
            g.Connect(n1, n2, w, "") |> ignore
        let result = g.Dijkstra(src, dst)
        (result.GetPath() |> Seq.toList, result.Distance)
