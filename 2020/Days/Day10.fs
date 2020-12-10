namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day10 =

    let onesAndThrees list =
        let rec calc l ones threes =
            match l with
            | n1 :: n2 :: ns -> if n2 = n1 + 1 
                                then calc (n2 :: ns) (ones+1) threes
                                else if n2 = n1 + 3
                                     then calc (n2 :: ns) ones (threes+1)
                                     else calc (n2 :: ns) ones threes
            | _              -> (ones, threes)
        calc list 0 0

    let Part1 _ = 
        let input = GetDataAsInts "Day10Input.txt" 
        let (ones,threes) = 0 :: (List.max input) + 3 :: input |> List.sort |> onesAndThrees
        ones * threes 

    let consGroups ns =
        let rec calc groups ack left =
            match left with
            | i1 :: i2 :: is when i2 = i1 + 1L -> calc groups (i1 :: ack) (i2 :: is)
            | i1 :: i2 :: is                   -> calc ((i1 :: ack) :: groups) [] (i2 :: is)
            | [i]                              -> (i :: ack) :: groups
            | _                                -> []
        calc [] [] ns

    let confsForSize = function
    | 5L -> 7L
    | 4L -> 4L
    | 3L -> 2L
    | 2L -> 1L
    | 1L -> 1L
    | x -> failwith "confsForSize only defined for 1-5"

    let Part2 _ = 
        let input = GetDataAsLongs "Day10Input.txt" 
        0L :: (List.max input) + 3L :: input |> 
        List.sort |> 
        consGroups  |> 
        List.map List.length |> 
        List.map int64 |> 
        List.map confsForSize |> 
        List.fold (fun s t -> s * t) 1L  
