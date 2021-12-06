namespace AdventOfCode2021

open Lib.Extensions.StringExtensions
open Lib.DataLoader
open Spec2021

module Day06 =

    let buildInitialList input = 
        let add (n : int) (m : Map<int, int64>) = if not (Map.containsKey n m) then Map.add n 0L m else m
        let map = List.countBy (fun i -> i) input |> List.map (fun (k, v) -> (k, int64 v)) |> Map
        map |> add 0 |> add 1 |> add 2 |> add 3 |> add 4 |> add 5 |> add 6 |> add 7 |> add 8 |> Map.toList |> List.map snd

    let step (fs : int64 list) = [fs.[1]; fs.[2]; fs.[3]; fs.[4]; fs.[5]; fs.[6]; fs.[7] + fs.[0]; fs.[8]; fs.[0]]

    let rec simulate n fs = if n = 0 then fs else simulate (n-1) (step fs)

    let runSimulation steps = 
        let input = getDataAsStringList (withPath "Day06Input.txt") |> List.head
        input.toInts ',' |> buildInitialList |> simulate steps |> List.sum

    let Part1 () = runSimulation 80

    let Part2 () = runSimulation 256
