namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day14 =

    let Part1 _ = 
        let interpretMask m =
            let mask = m |> Seq.toArray 
            let bits = seq { 0 .. Array.length mask - 1 } |> Seq.toArray |> Array.rev 
            mask |> Array.zip bits
        let applyBitmask (value : int64) m =
            let apply (value : int64) (pos, op) = 
                let mask = 1L <<< pos
                match op with
                | '0' -> value &&& (~~~mask)
                | '1' -> value ||| mask
                | _   -> value
            Array.fold apply value m
        let applyLine (mask, memory) line =
            let set pos v = Map.add pos (applyBitmask (int64 v) mask) memory
            match line with
            | Regex @"^mem\[([0-9]+)\] = ([0-9]+)$" [ pos; v ] -> (mask, set pos v)
            | Regex @"^mask = ([X|0|1]{36})$" [ m ]            -> (interpretMask m, memory)
            | _                                                -> (mask, memory)
        GetDataAsStringList "Day14Input.txt" |> 
        List.fold applyLine (Array.empty, Map.empty) |> 
        snd |> Map.toList |> List.map snd |> List.sum
    

    let Part2 _ = 
        let int64ToBitMask i = 
            let m = int64ToBitChars i
            let prefix = Array.create (36 - String.length m) '0' |> System.String
            prefix + m
        let applyMask bitString mask =
            let modifyBit fromValue fromMask =
                match fromMask with
                | 'X' -> 'X'
                | '1' -> '1'
                | _   -> fromValue
            mask |> Array.zip bitString |> Array.map (fun (a, b) -> modifyBit a b )
        let rec allBitmasks bitmask =
            if Seq.isEmpty bitmask 
            then [[]]
            else let prepend n = List.map (fun tl -> List.Cons (n, tl)) (allBitmasks (Seq.tail bitmask))
                 match Seq.head bitmask with
                 | '0' -> prepend '0'
                 | '1' -> prepend '1'
                 | _   -> List.concat [prepend '0'; prepend '1']
        let allMemoryPositions mem mask =
            applyMask (int64ToBitMask mem |> Seq.toArray) (mask |> Seq.toArray) |> allBitmasks |> List.map bitCharsToInt64
        let applyLine2 (mask, memory) line =
            let set pos v = 
                let set2 m pos = Map.add pos v m
                let mems = allMemoryPositions pos mask
                List.fold set2 memory mems
            match line with
            | Regex @"^mem\[([0-9]+)\] = ([0-9]+)$" [ pos; v ] -> (mask, set (int64 pos) (int64 v))
            | Regex @"^mask = ([X|0|1]{36})$" [ m ]            -> (m, memory)
            | _                                                -> (mask, memory)
        GetDataAsStringList "Day14Input.txt" |> 
        List.fold applyLine2 ("", Map.empty) |>
        snd |> Map.toList |> List.map snd |> List.sum
