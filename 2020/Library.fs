namespace AdventOfCode2020

open System.IO
open System.Text.RegularExpressions

module Library =

    let private dataFile fileName = 
        let dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\2020\Data\")
        Path.Combine(dir, fileName)

    let GetDataAsStringArray fileName = File.ReadAllLines (dataFile fileName)

    let GetDataAsStringList fileName = File.ReadAllLines (dataFile fileName) |> Array.toList

    let GetDataAsCharLists fileName = GetDataAsStringList fileName |> List.map Seq.toList

    let GetDataAsCharArrays fileName = GetDataAsStringArray fileName |> Array.map Seq.toArray

    let GetDataAsInts fileName = GetDataAsStringList fileName |> List.map int

    let GetDataAsLongs fileName = GetDataAsStringList fileName |> List.map int64

    // [1, 2, 3] -> sequence (1, 2), (1, 3), (2, 3)
    let rec pairs l = seq {  
        match l with 
        | h :: t -> for e in t do yield h, e
                    yield! pairs t
        | _      -> () } 

    // [1, 2, 3, 4] -> sequence (1, 2, 3), (1, 2, 4), (2, 3, 4)
    let rec triplets l = seq {
        match l with 
        | h :: t -> for (a, b) in pairs t do yield h, a, b
                    yield! triplets t
        | _ -> () } 

    // Usage: see Day02.fs
    let (|Regex|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
        else None

    // Example: count 'd' "Advent of code" will return 2
    let count x = Seq.filter ((=) x) >> Seq.length

    // Usage: see Day08.fs
    let (|Prefix|_|) (p:string) (s:string) =
        if s.StartsWith(p) then
            Some(s.Substring(p.Length))
        else
            None

    let splitBy f input =
        let i = ref 0
        input
        |> Seq.groupBy (fun x ->
            if f x then incr i
            !i)
        |> Seq.map snd
