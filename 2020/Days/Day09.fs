namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day09 =

    let okSum prev num = List.allPairs prev prev |> List.tryFind (fun (a,b) -> a <> b && a + b = num) |> Option.isSome

    let rec findFirstNotOk (preamble : int64 list) (data : int64 list) =
        match data with
        | n :: ns when okSum preamble n -> findFirstNotOk (List.skip 1 preamble @ [n]) ns
        | n :: _                        -> n
        | _                             -> 0L

    let Part1 _ = 
        let input = GetDataAsLongs "Day09Input.txt"
        let preamble = List.take 25 input
        let data = List.skip 25 input
        findFirstNotOk preamble data

    let getSubSequences (data : int64 list) = 
        seq { for i in 1 .. List.length data do yield (List.skip (i-1) data) }

    let rec startsWithSum (num : int64) (data : int64 list) : Option<int64 list> =
        let rec sws (d : int64 list) (nu : int64) (a : int64 list) =
            match d with
            | n :: ns when n = nu -> Some (a @ [n])
            | n :: ns when n < nu -> sws ns (nu-n) (a @ [n])
            | _                   -> None 
        sws data num List.empty

    let Part2 _ = 
        let target = Part1 0
        let list = GetDataAsLongs "Day09Input.txt" |> getSubSequences |> Seq.choose (startsWithSum target) |> Seq.head
        List.min list + List.max list
