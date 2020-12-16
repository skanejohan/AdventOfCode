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

    let lastElement list = List.reduce (fun _ i -> i) list

    let rec int64ToBitChars (i : int64) =
        match i with
        | 0L | 1L -> string i
        | _ ->
            let bit = string (i % 2L)
            (int64ToBitChars (i / 2L)) + bit

    let bitCharsToInt64 (bits : char list) =
        let rec raise2 n = 
            match n with
            | 0L -> 1L
            | x  -> 2L * raise2 (n-1L)
        List.rev bits |>
        List.map (fun x -> int x - 48) |>
        List.zip (List.map int64 [0..List.length bits - 1]) |>
        List.map (fun (a,b) -> (raise2 a) * int64 b) |>
        List.sum

    let rec flatten list =
        match list with
        | l :: ls -> l @ flatten ls
        | _       -> []

    // [[5; 14; 9; 12]; [15; 1; 5; 1]; [3; 9; 18; 7]] -> [[5; 15; 3]; [14; 1; 9]; [9; 5; 18]; [12; 1; 7]]
    let rotateLists lists = 
        let rec combine list1 list2 = 
            match list1 with
            | x :: xs -> match list2 with 
                         | y :: ys -> (x @ y) :: combine xs ys
                         | _       -> []
            | _       -> []
        let combine2 lists = List.fold (fun s a -> combine s a) (List.head lists) (List.tail lists)
        let xx = List.map (List.map (fun a -> [a])) lists
        combine2 xx
