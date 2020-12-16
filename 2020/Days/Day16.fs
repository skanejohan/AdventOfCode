namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day16 =

    let parseInput lines = 
        let rec parse rules tickets lines =
            let rulesRegExp = @"[a-z ]+\: ([0-9]+)[-]([0-9]+) or ([0-9]+)[-]([0-9]+)"
            match lines with
            | l :: ls -> match l with
                         | Regex rulesRegExp [ min1; max1; min2; max2 ] -> parse (List.Cons ((int min1, int max1), List.Cons ((int min2, int max2), rules))) tickets ls
                         | Regex @"(\d+)(,\d+)*" [ _; _ ]               -> let nums = l.Split "," |> Array.map int |> Array.toList
                                                                           parse rules (nums @ tickets) ls 
                         | _                                            -> parse rules tickets ls
            | _       -> let l = List.length tickets
                         let your = List.skip (l-3) tickets
                         let nearby = List.take (l-3) tickets
                         (rules, your, nearby)
        parse [] [] lines

    let isValid rules n = List.exists (fun (min,max) -> min <= n && max >= n) rules

    let isInvalid rules n = not (isValid rules n)

    let Part1 _ = 
        let data = GetDataAsStringList "Day16Input.txt"
        let (rules, your, nearby) = parseInput data
        let invalidNearby = nearby |> List.filter (isInvalid rules)
        invalidNearby |> List.sum
    
    let Part2 _ = 0
