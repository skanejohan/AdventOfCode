namespace Lib

open System.IO

open Collections.BitLists

open Lib.Utils.CharUtils

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

    let getDataAsOneBasedArray2D fileName = 
        let input = getDataAsCharArrays fileName
        let w = Array.length input.[0]
        let h = Array.length input
        Array2D.initBased 1 1 w h (fun i j -> input.[j-1].[i-1])

    let getDataAsOneBasedIntArray2D fileName = 
        let input = getDataAsCharArrays fileName
        let w = Array.length input.[0]
        let h = Array.length input
        Array2D.initBased 1 1 w h (fun i j -> charToNum input.[j-1].[i-1])
