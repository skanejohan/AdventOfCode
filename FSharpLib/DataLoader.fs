namespace Lib

open System.IO

open BitLists

module DataLoader =

    let getDataAsStringArray fileName = File.ReadAllLines fileName

    let getDataAsStringList fileName = getDataAsStringArray fileName |> Array.toList

    let getDataAsCharLists fileName = getDataAsStringList fileName |> List.map Seq.toList

    let getDataAsBitLists fileName = getDataAsCharLists fileName |> BitLists.fromCharLists

    let getDataAsCharArrays fileName = getDataAsStringArray fileName |> Array.map Seq.toArray

    let getDataAsInts fileName = getDataAsStringList fileName |> List.map int

    let getDataAsLongs fileName = getDataAsStringList fileName |> List.map int64

    let stringToInts (s : string) c = s.Split [|c|] |> Array.toList |> List.map int // TODO move
