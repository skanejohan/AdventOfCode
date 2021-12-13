namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Regex

module Day13 =

    let parseLine (coords, instr) line = 
        match line with
        | Regex @"(\d+),(\d+)" [ x; y ]     -> ((int x, int y) :: coords, instr)
        | Regex @"fold along x=(\d+)" [ n ] -> (coords, ("X", int n) :: instr)
        | Regex @"fold along y=(\d+)" [ n ] -> (coords, ("Y", int n) :: instr)
        | _                                 -> (coords, instr)

    let newCoord (dim, n) (x, y) = 
        let foldX = if x < n then (x, y) else (2 * n - x, y)
        let foldY = if y < n then (x, y) else (x, 2 * n - y)
        if dim = "Y" then foldY else foldX

    let getInput = getDataAsStringList (Spec2021.withPath "Day13Input.txt") |> List.fold parseLine ([], []) |> fun (s, l) -> (s, List.rev l)

    let pointIsOnFold (x, y) (dim, n) = if dim = "Y" then y = n else x = n

    let fold coords instr = coords |> List.map (newCoord instr) |> Set |> Set.toList |> List.filter (fun p -> not (pointIsOnFold p instr))

    let Part1 () = 
        let (coords, instr) = getInput
        fold coords (List.head instr) |> List.length

    let visualize coords = 
        let minX = Seq.map fst coords |> Seq.min
        let minY = Seq.map snd coords |> Seq.min
        let maxX = Seq.map fst coords |> Seq.max
        let maxY = Seq.map snd coords |> Seq.max
        Array2D.init (maxX - minX + 1) (maxY - minY + 1) (fun x y -> if Seq.contains (x, y) coords then "*" else " ")

    let Part2 () = 
        let (coords, instr) = getInput
        let result = instr |> List.fold fold coords
        printfn "%A" (visualize result)
        "PFKLKCFP"