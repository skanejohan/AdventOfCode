namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day11 =

    let occupied map (row, col) = 
        match CharMap.TryFind row col map with
        | Some '#' -> true
        | _        -> false
    
    let updateCell row col old fn map =
        let n = fn row col map
        match CharMap.Get row col map with
        | 'L' when n = 0 -> '#'
        | '#' when n > 3 -> 'L'
        | _              -> old
    
    let allPositions rows cols = List.allPairs [0..rows-1] [0..cols-1]

    let stepOne fn map = 
        let rec update positions m = 
            match positions with
            | (r, c) :: ps -> let oldChar = CharMap.Get r c map
                              let newChar = updateCell r c oldChar fn map
                              update ps (CharMap.Set r c newChar m)
            | _            -> m
        update (allPositions map.rows map.cols) map

    let steps fn map = 
        let step (b,m) = 
            let nextM = stepOne fn m
            Some ((b,m), (m <> nextM, nextM))
        Seq.unfold step map

    let run fn =
        GetDataAsCharArrays "Day11Input.txt" |> 
        CharMap.CreateFromCharArray |>
        (fun map -> Seq.takeWhile fst (steps fn (true,map))) |>
        Seq.toList |> 
        lastElement |> 
        snd |>
        CharMap.Count '#'

    let Part1 _ = 
        let neighborPositions row col = 
            [(row-1,col-1); 
             (row-1,col); 
             (row-1,col+1); 
             (row,col+1); 
             (row+1,col+1); 
             (row+1,col); 
             (row+1,col-1); 
             (row,col-1)]
        let noOfOccupiedNeighbors row col map = 
            neighborPositions row col |>
            List.map (occupied map) |>
            List.filter id |>
            List.length
        run noOfOccupiedNeighbors
    
    let Part2 _ =
        0
