namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Regex
open Lib.Utils.MapUtils
open Lib.Utils.StringUtils

module Day14 =

    let getInput fileName =
        let parseLine s = 
            match s with
            | Regex @"(.)(.) -> (.)" [ c1; c2; o ] -> ((stringToChar c1, stringToChar c2), stringToChar o)
            | _                                    -> failwith "nope"
        let input = getDataAsStringList (Spec2021.withPath fileName)
        let polymerTemplate = List.head input |> stringToCharList
        let insertionRules = List.tail (List.tail input) |> List.map parseLine |> Map
        (polymerTemplate, insertionRules)

    let initTokenCount tokens insertionRules=
        let rec tuples ts = match ts with
                            | t1 :: t2 :: tss -> (t1, t2) :: tuples (t2 :: tss)
                            | _               -> []
        let rec applyTuples tuples map =
            match tuples with
            | t :: ts -> applyTuples ts (incCountL t map)
            | _       -> map
        let emptyTokens = Map.map (fun k v -> 0L) insertionRules
        applyTuples (tuples tokens) emptyTokens

    let step tokenCount rules =
        let rec step' td map =
            match td with
            | ((c1, c2), n) :: ts -> let newMap = addCountL (c1, c2) 0L map
                                     if n = 0L
                                     then step' ts newMap
                                     else let midChar = Map.find (c1, c2) rules
                                          let part1 = (c1, midChar)
                                          let part2 = (midChar, c2)
                                          step' ts (addCountL part2 n (addCountL part1 n newMap))
            | _                   -> map
        step' (Map.toList tokenCount) Map.empty 
    
    let rec stepN c tokenCount rules =
        match c with
        | 0 -> tokenCount
        | n -> stepN (c-1) (step tokenCount rules) rules

    let letterCount tokenCount = 
        tokenCount 
        |> Map.toList 
        |> List.map (fun ((c1, c2), n) -> (c1, n)) 
        |> List.groupBy (fun (c, n) -> c) 
        |> List.map snd
        |> List.map (List.sumBy snd)

    let run n fileName = 
        let (pt, ir) = getInput fileName
        let lastChar = pt |> List.rev |> List.head
        let tokenCount = initTokenCount pt ir
        let result = stepN n tokenCount ir |> Map.add (lastChar, '*') 1L
        let counts = letterCount result
        List.max counts - List.min counts

    let Part1 () = run 10 "Day14Input.txt"

    let Part2 () = run 40 "Day14Input.txt"
