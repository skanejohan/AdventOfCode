namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections

module Day02 =

    let private passwordOk1 (pwd, c, min, max) = 
        let n = count c pwd
        n >= min && n <= max
    
    let private passwordOk2 ((pwd : string), c, min, max) = 
        let v1 = if pwd.[min-1] = c then 1 else 0
        let v2 = if pwd.[max-1] = c then 1 else 0
        v1 + v2 = 1

    let private parseLine line = 
        match line with
        | Regex @"([0-9]+)[-]([0-9]+)[ ]([a-z]{1})[:][ ]([a-z]+)" [ min; max; c; pwd ] -> (pwd, char c, int min, int max)
        | _                                                                            -> ("", ' ', 0, 0)

    let private lineOk predicate line = parseLine line |> predicate

    let Part1 _ = GetDataAsStringList "Day02Input.txt" |> List.filter (lineOk passwordOk1) |> List.length

    let Part2 _ = GetDataAsStringList "Day02Input.txt" |> List.filter (lineOk passwordOk2) |> List.length


