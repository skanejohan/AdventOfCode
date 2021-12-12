namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Extensions.ListExtensions
open Lib.Utils

module Day11 =

    let step map =
        let rec flash flashes m = 
            let flashing = m |> Array2dUtils.toList |> List.filter (fun (_, _, v) -> v > 9) |> List.map (fun (x, y, _) -> (x, y))
            let flashCount = flashes + List.length flashing
            let zeroedMap = Array2D.mapi (fun x y v -> if List.contains (x, y) flashing then 0 else v) m
            let neighbors = List.map (Array2dUtils.validNeighbors8 zeroedMap) flashing 
                            |> List.flatten
                            |> List.filter (fun pos -> not (List.contains pos flashing))
                            |> List.fold (fun m (x, y) -> MapUtils.incCount (x, y) m) Map.empty
            let updatedMap = Array2D.mapi (fun x y v -> if v = 0 then 0 else v + MapUtils.getCount (x, y) neighbors) zeroedMap
            if Map.count neighbors = 0 then (updatedMap, flashCount) else flash flashCount updatedMap
        Array2D.map ((+) 1) map |> flash 0

    let stepN count map = 
        let rec stepN' c flashes m =
            match c with
            | 0 -> (m, flashes)
            | n -> let (m', f') = step m
                   stepN' (c-1) (flashes+f') m'
        stepN' count 0 map

    let stepUntilAllFlash map = 
        let rec stepUntilAllFlash' m n =
            let (m', f') = step m
            if Array2dUtils.count m' = f' then n
            else stepUntilAllFlash' m' (n+1)
        stepUntilAllFlash' map 1

    let Part1 () = 
        let initialMap = getDataAsArray2D (Spec2021.withPath "Day11Input.txt") 
                         |> Array2D.map (fun c -> int c - int '0')
        let (map, flashes) = stepN 100 initialMap
        flashes

    let Part2 () = getDataAsArray2D (Spec2021.withPath "Day11Input.txt") |> Array2D.map (fun c -> int c - int '0') |> stepUntilAllFlash
