namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day11 =
   
    let allPositions rows cols = List.allPairs [0..rows-1] [0..cols-1]

    let stepOne fn map = 
        let rec update positions m = 
            match positions with
            | (r, c) :: ps -> let oldChar = CharMap.Get r c map
                              let newChar = fn r c oldChar map
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
        let occupied map (row, col) = 
            match CharMap.TryFind row col map with
            | Some '#' -> true
            | _        -> false
        let updateCell1 r c old map =
            let n = 
                [(r-1,c-1); (r-1,c); (r-1,c+1); (r,c+1); (r+1,c+1); (r+1,c); (r+1,c-1); (r,c-1)] |>
                List.map (occupied map) |>
                List.filter id |>
                List.length
            match CharMap.Get r c map with
            | 'L' when n = 0 -> '#'
            | '#' when n > 3 -> 'L'
            | _              -> old
        run updateCell1

    let Part2 _ =
        let rec findOccupiedChairs row col map =
            let rec findOccupiedChair r c (dr, dc) = 
                match CharMap.TryFind (r+dr) (c+dc) map with
                | Some 'L' -> None
                | Some '#' -> Some (r+dr, c+dc)
                | Some '.' -> findOccupiedChair (r+dr) (c+dc) (dr, dc) 
                | _        -> None
            [(-1,-1); (-1,0); (-1,1); (0,1); (1,1); (1,0); (1,-1); (0,-1)] |>
            List.map (findOccupiedChair row col) |> 
            List.filter Option.isSome
            |> List.map Option.get
        let updateCell2 r c old map =
            let n = findOccupiedChairs r c map |> List.length
            match CharMap.Get r c map with
            | 'L' when n = 0 -> '#'
            | '#' when n > 4 -> 'L'
            | _              -> old
        run updateCell2
