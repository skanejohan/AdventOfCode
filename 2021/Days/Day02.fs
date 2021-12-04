namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Regex
open Spec2021

module Day02 =

    let private parseLine line = 
        match line with
        | Regex @"[f][o][r][w][a][r][d] (\d+)" [ n ] -> ("F", int n)
        | Regex @"[d][o][w][n] (\d+)" [ n ]          -> ("D", int n)
        | Regex @"[u][p] (\d+)" [ n ]                -> ("U", int n)
        | _                                          -> ("", 0)

    let Part1 () = 
        let input = getDataAsStringList (withPath "Day02Input.txt")
        let f = input |> List.map parseLine |> List.filter (fun (a, b) -> a = "F") |> List.map snd |> List.sum
        let d = input |> List.map parseLine |> List.filter (fun (a, b) -> a = "D") |> List.map snd |> List.sum
        let u = input |> List.map parseLine |> List.filter (fun (a, b) -> a = "U") |> List.map snd |> List.sum
        f * (d - u)

    let Part2 () =
        let input = getDataAsStringList (withPath "Day02Input.txt")
        let moves = input |> List.map parseLine
        let rec move moves hor_pos depth aim =
            match moves with
            | m :: ms -> match m with
                         | ("D", n) -> move ms hor_pos depth (aim + n)
                         | ("U", n) -> move ms hor_pos depth (aim - n)
                         | ("F", n) -> move ms (hor_pos + n) (depth + aim * n) aim
                         | _        -> failwith "nope"
            | _       -> hor_pos * depth
        move moves 0 0 0
