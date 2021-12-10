namespace Lib

open System.IO

open Collections.BitLists

module DataLoader =

    let getDataAsStringArray fileName = File.ReadAllLines fileName

    let getDataAsStringList fileName = getDataAsStringArray fileName |> Array.toList

    let getDataAsCharLists fileName = getDataAsStringList fileName |> List.map Seq.toList

    let getDataAsBitLists fileName = getDataAsCharLists fileName |> BitLists.fromCharLists

    let getDataAsCharArrays fileName = getDataAsStringArray fileName |> Array.map Seq.toArray

    let getDataAsInts fileName = getDataAsStringList fileName |> List.map int

    let getDataAsLongs fileName = getDataAsStringList fileName |> List.map int64

    let getDataAsString fileName = getDataAsStringList fileName |> List.head

    let getDataAsArray2D fileName = 
        let input = getDataAsCharArrays fileName
        let w = Array.length input.[0]
        let h = Array.length input
        Array2D.initBased 0 0 w h (fun i j -> input.[j].[i])
