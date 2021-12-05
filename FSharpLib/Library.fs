namespace Lib

module Library =

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

    let intsToInt64 (bits : int list) =
        let rec raise2 n = 
            match n with
            | 0L -> 1L
            | x  -> 2L * raise2 (n-1L)
        List.rev bits |>
        List.zip (List.map int64 [0..List.length bits - 1]) |>
        List.map (fun (a,b) -> (raise2 a) * int64 b) |>
        List.sum

    let intersection lists = 
        List.fold (fun s l -> Set.intersect s (set l)) (List.head lists |> set) lists

    let get key map def =
        match Map.tryFind key map with
        | Some v -> v
        | None   -> def

