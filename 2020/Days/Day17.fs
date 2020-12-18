namespace AdventOfCode2020

open Library
open AdventOfCode
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day17 =

    let getData file dimensions =
        let zeros = List.init (dimensions-2) (fun _ -> 0)
        let rec addRow x y row (ack : GridN.IntGrid) =
            match row with 
            | '#' :: cs -> addRow (x+1) y cs (GridN.IntGrid.add (x :: y :: zeros) ack)
            | _ :: cs   -> addRow (x+1) y cs ack
            | _         -> ack 
        let rec addRows y rows (ack : GridN.IntGrid) =
            match rows with
            | r :: rs -> addRows (y+1) rs (addRow 0 y r ack)
            | _       -> ack 
        addRows 0 (GetDataAsCharLists file) (GridN.IntGrid.empty dimensions)

    let activeNeighbors coords state = GridN.neighborCoords coords |> List.filter (fun a -> Set.contains a state) |> List.length
                
    let isAlive coords cells = Set.contains coords cells

    let evolveCell coords cells = 
        let n = activeNeighbors coords cells
        if isAlive coords cells
        then n = 2 || n = 3
        else n = 3

    let updateCell coords (oldGrid : GridN.IntGrid) (grid : GridN.IntGrid) = 
        if evolveCell coords oldGrid.Alive
        then GridN.IntGrid.add coords grid 
        else GridN.IntGrid.remove coords grid

    let step dimensions grid = 
        let folder g coords = updateCell coords grid g
        let allCombs = List.zip grid.MinCoords grid.MaxCoords |> 
                       List.map (fun (a, b) -> [a-1..b+1]) |>
                       GridN.allCombinations
        List.fold folder (GridN.IntGrid.empty dimensions) allCombs

    let calculate dimensions = 
        let grid = getData "Day17Input.txt" dimensions |> 
                   step dimensions |> 
                   step dimensions |> 
                   step dimensions |> 
                   step dimensions |> 
                   step dimensions |> 
                   step dimensions
        Set.count grid.Alive

    let Part1 _ = calculate 3

    let Part2 _ = calculate 4
