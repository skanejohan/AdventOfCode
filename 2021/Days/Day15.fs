namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Utils.Array2dUtils
open Lib.Utils.Dijkstra
open Lib.Utils.NumUtils

module Day15 =

    let getNodesAndEdges input = 
        let w = Array2D.length1 input
        let h = Array2D.length2 input
        let nodes = [1u..uint32 (w * h)]
        let edges = 
            let coordToIndex (x, y) = w * (y-1) + x |> uint32
            let calcEdges x y c = 
                let u = if y > 1 then [((x, y-1), (x, y), c)] else []
                let r = if x < w then [((x+1, y), (x, y), c)] else []
                let d = if y < h then [((x, y+1), (x, y), c)] else []
                let l = if x > 1 then [((x-1, y), (x, y), c)] else []
                [u; r; d; l] |> List.concat
            Array2D.mapi calcEdges input
            |> toList 
            |> List.map (fun (_, _, v) -> v) 
            |> List.concat
            |> List.map (fun (a, b, c) -> (coordToIndex a, coordToIndex b, c))
        (nodes, edges)

    let Part1 () = 
        let input = getDataAsOneBasedIntArray2D (Spec2021.withPath "Day15Input.txt") 
        let (nodes, edges) = getNodesAndEdges input
        let (_, cost) = dijkstra nodes edges (List.min nodes) (List.max nodes)
        cost

    let multiply (input : int[,]) =
        let w = Array2D.length1 input
        let h = Array2D.length2 input
        let calcX (input : int[,]) x y = 
            if x <= w 
            then input.[x, y]
            else let xx = wrap x w
                 let dx = (x-1) / w
                 wrap (input.[xx, y] + dx) 9
        let calcY (input : int[,]) x y = 
            if y <= h 
            then input.[x, y]
            else let yy = wrap y h
                 let dy = (y-1) / h
                 wrap (input.[x, yy] + dy) 9
        let expandedX = Array2D.initBased 1 1 (5*w) h (calcX input)
        Array2D.initBased 1 1 (5*w) (5*h) (calcY expandedX)

    let Part2 () =
        let input = getDataAsOneBasedIntArray2D (Spec2021.withPath "Day15Input.txt") 
        let (nodes, edges) = getNodesAndEdges (multiply input)
        let (_, cost) = dijkstra nodes edges (List.min nodes) (List.max nodes)
        cost
