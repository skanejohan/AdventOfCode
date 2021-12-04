namespace AdventOfCode2018

open Library

module Day02 =

    let Part1 () = 
        let boxIds = GetDataAsCharLists "Day02Input.txt" |> List.toArray
        let rec count s map =
            match s with
            | c :: cs -> count cs (Map.change c (fun s -> 
                                                 match s with
                                                 | Some s -> Some (s + 1)
                                                 | None   -> Some 1) map)
            | _       -> map
        let has n map = Map.filter (fun k v -> v = n) map |> Map.count > 0
        let has2 = Array.filter (fun s -> has 2 (count s Map.empty)) boxIds
        let has3 = Array.filter (fun s -> has 3 (count s Map.empty)) boxIds
        Array.length has2 * Array.length has3

    let Part2 () = 
        let boxIds = GetDataAsCharLists "Day02Input.txt" 
        let isMatch s candidate =  Seq.length (Seq.map2((=)) s candidate |> Seq.filter (fun t -> t)) = (Seq.length s) - 1
        let rec findMatchFor s ss =
            match ss with
            | c :: cs when isMatch s c -> Some (s, c)
            | c :: cs                  -> findMatchFor s cs
            | _                        -> None
        let rec findMatch ss = 
            match ss with
            | c :: cs -> match findMatchFor c cs with
                         | Some x -> Some x
                         | None -> findMatch (List.tail ss)
            | _       -> None 
        //let x = findMatch boxIds
        let x = findMatch ["abcde"; "fghij"; "klmno"; "pqrst"; "fguij"; "axcye"; "wvxyz" ]
        0

    //let boxIds = [|"abcdef"; "bababc"; "abbcde"; "abcccd"; "aabcdd"; "abcdee"; "ababab" |] |> Array.map Seq.toArray
