namespace AdventOfCode2021

open System.IO

open Spec2021
open BitList

module Data =

    let getDataAsStringArray fileName = File.ReadAllLines (dataFile fileName)

    let getDataAsStringList fileName = File.ReadAllLines (dataFile fileName) |> Array.toList

    let getDataAsCharLists fileName = getDataAsStringList fileName |> List.map Seq.toList

    let getDataAsBitLists fileName = getDataAsCharLists fileName |> charListsToBitLists

    let getDataAsCharArrays fileName = getDataAsStringArray fileName |> Array.map Seq.toArray

    let getDataAsInts fileName = getDataAsStringList fileName |> List.map int

    let getDataAsLongs fileName = getDataAsStringList fileName |> List.map int64

    let stringToInts (s : string) c = s.Split [|c|] |> Array.toList |> List.map int
