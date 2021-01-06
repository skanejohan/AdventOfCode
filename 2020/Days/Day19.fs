namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day19 =

    type Rule = 
    | Either of int list * int list
    | All of int list
    | Char of char

    let parse lines =
        let toInts (s : string) = s.Split ' ' |> Array.toList |> List.map int
        let rec doParse inputs rules lines =
            match lines with
            | l :: ls -> match l with
                         | Regex @"(\d+): ([^|]+) \| ([^|]+)" [n; a1; a2 ] -> doParse inputs (Map.add (int n) (Either (toInts a1, toInts a2)) rules) ls
                         | Regex "(\\d+): \"(.)\"" [n; c]                  -> doParse inputs (Map.add (int n) (Char (char c)) rules) ls
                         | Regex "(\d+): (.+)" [n; a]                      -> doParse inputs (Map.add (int n) (All (toInts a)) rules) ls
                         | Regex "(.+)" [s]                                -> doParse ((s |> Seq.toList) :: inputs) rules ls
                         | _                                               -> doParse inputs rules ls
            | _       -> (rules, inputs) 
        doParse [] Map.empty lines
        
    let rec eval chars rule allRules =
        let getRule r = Map.find r allRules
        let evalChar chars ch =
            match chars with
            | c :: cs when c = ch -> (true, cs)
            | _                   -> (false, chars) 
        let rec evalAll chars rules =
            match rules with
            | r :: rs -> match (eval chars (getRule r) allRules) with
                         | (false, _) -> (false, chars)
                         | (true, cs) -> evalAll cs rs
            | _       -> (true, chars)
        let evalEither chars rules1 rules2 = 
            match evalAll chars rules1 with
            | (true, s) -> (true, s)
            | _         -> evalAll chars rules2
        match rule with 
        | Either (rules1, rules2) -> evalEither chars rules1 rules2
        | All rules               -> evalAll chars rules
        | Char ch                 -> evalChar chars ch

    let passesRule0 rules chars =
        let (ok, rem) = eval chars (Map.find 0 rules) rules
        ok && List.length rem = 0

    let Part1 () =
        let (rules, inputs) = GetDataAsStringList "Day19Input.txt" |> parse
        inputs |> List.filter (passesRule0 rules) |> List.length

    let Part2 () =
        0
