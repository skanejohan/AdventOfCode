namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day19 =

    type Rule = 
    | Either of int list list
    | All of int list
    | Char of char

    let parse lines =
        let toInts (s : string) = s.Split ' ' |> Array.toList |> List.map int
        let rec doParse inputs rules lines =
            match lines with
            | l :: ls -> match l with
                         | Regex @"(\d+): ([^|]+) \| ([^|]+)" [n; a1; a2 ] -> doParse inputs (Map.add (int n) (Either [toInts a1; toInts a2]) rules) ls
                         | Regex "(\\d+): \"(.)\"" [n; c]                  -> doParse inputs (Map.add (int n) (Char (char c)) rules) ls
                         | Regex "(\d+): (.+)" [n; a]                      -> doParse inputs (Map.add (int n) (All (toInts a)) rules) ls
                         | Regex "(.+)" [s]                                -> doParse ((s |> Seq.toList) :: inputs) rules ls
                         | _                                               -> doParse inputs rules ls
            | _       -> (rules, inputs) 
        doParse [] Map.empty lines
        
    let rec eval chars rule allRules =
        let getRule r = Map.find r allRules
        
        let evalChar (chars : char list) (ch : char) =
            match chars with
            | c :: cs when c = ch -> (true, cs)
            | _                   -> (false, chars) 
        let rec evalAll (chars : char list) rules =
            match rules with
            | r :: rs -> match (eval chars (getRule r) allRules) with
                            | (false, _) -> (false, chars)
                            | (true, cs) -> evalAll cs rs
            | _       -> (true, chars)
        let rec evalEither (chars : char list) rules =
            match rules with
            | r :: rs -> match evalAll chars r with
                         | (true, s) -> (true, s)
                         | _         -> evalEither chars rs
            | _       -> (false, chars)
        match rule with 
        | Either rules -> evalEither chars rules
        | All rules    -> evalAll chars rules
        | Char ch      -> evalChar chars ch

    let passesRule0 rules chars =
        let (ok, rem) = eval chars (Map.find 0 rules) rules
        ok && List.length rem = 0

    let Part1 () = 
        let (rules, inputs) = GetDataAsStringList "Day19Input.txt" |> parse
        inputs |> List.filter (passesRule0 rules) |> List.length

    let Part2 () =
        // Looking at the data, I realize that the top-level rule is one or more "42" followed by rule 11.
        // The fact that I can modify the top-level rule is good because than I can properly try all the 
        // different options if it fails. If a rule further down was affected it would be much more difficult
        // since even if that rule passed, the top-level rule might fail because another of the sub-rule's
        // options was the one that should have been chosen.
        let data = GetDataAsStringList "Day19Input.txt"
        let replace s = 
            match s with
            | "11: 42 31" -> "11: 42 11 31 | 42 31"
            | s           -> s
        let (rules, inputs) = data |> List.map replace |> parse
        let newRule0 = Either [ [42; 11]; [42; 42; 11]; [42; 42; 42; 11]; [42; 42; 42; 42; 11]; [42; 42; 42; 42; 42; 11]]
        let newRules = Map.add 0 newRule0 rules
        inputs |> List.filter (passesRule0 newRules) |> List.length
