namespace AdventOfCode2020

open Library

module Day01 =

    let Part1 _ = 
        let allPairs = pairs (GetDataAsInts "Day01Input.txt") |> Seq.toList
        let (a, b) = List.find (fun (a, b) -> a + b = 2020) allPairs
        a * b

    let Part2 _ =
        let allTriplets = triplets (GetDataAsInts "Day01Input.txt") |> Seq.toList
        let (a, b, c) = List.find (fun (a, b, c) -> a + b + c = 2020) allTriplets
        a * b * c
