﻿namespace AdventOfCode2020

open System.IO

module Library =

    let private dataFile fileName = 
        let dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\2020\Data\")
        Path.Combine(dir, fileName)

    let GetDataAsStringList fileName = File.ReadAllLines (dataFile fileName) |> Array.toList

    let GetDataAsInts fileName = GetDataAsStringList fileName |> List.map int

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
