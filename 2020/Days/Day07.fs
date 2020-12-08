namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day07 =

    let split (sep : string) (s : string) = s.Split sep

    let replace (s1 : string) (s2 : string) (s : string) = s.Replace(s1, s2)

    // 6 dotted black -> Some (6, "dotted black"), "no other" -> None
    let parseContents s =
        if s = "no other" 
        then None 
        else 
            let parts = split " " s
            Some (int parts.[0], String.concat " " [parts.[1]; parts.[2]])

    let parseRule (s : string) = 
        let s2 = split " contain " s
        let key = 
            s2.[0] |> 
            replace " bags" "" |> 
            replace " bag" ""
        let value = 
            split ", " s2.[1] |> 
            Array.map (replace " bags" "") |> 
            Array.map (replace " bag" "") |> 
            Array.map (replace "." "") |>
            Array.map parseContents |> 
            Array.filter Option.isSome |> 
            Array.map Option.get
        (key, value)

    let addRule (map : Map<string, (int * string) []>) rule = parseRule rule |> map.Add

    let createHoldingList _ = GetDataAsStringList("Day07Input.txt") |> List.fold addRule Map.empty
    
    let rec canContainBag held (holdingList : Map<string, string[]>) holder =
        if Array.contains held (holdingList.Item holder) then true
        else Array.exists (fun bag -> canContainBag held holdingList bag) (holdingList.Item holder)

    let contains holder (holdingList : Map<string, (int * string) []>) =
        let rec cont holder = 
            let holds = holdingList.Item(holder)
            let sumRec = fun (n, s) -> n * cont s
            1 + Array.sumBy sumRec holds
        cont holder - 1

    let Part1 _ = 
        let holdingList = createHoldingList () |> Map.map (fun k v -> (Array.map snd) v)
        let allBags = holdingList |> Map.toSeq |> Seq.map fst
        allBags |> Seq.filter (fun s -> canContainBag "shiny gold" holdingList s) |> Seq.length

    let Part2 _ =
        let holdingList = createHoldingList ()
        contains "shiny gold" holdingList
        