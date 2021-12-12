namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Extensions.ListExtensions
open Lib.Regex
open Lib.Utils.MapUtils

module Day12 =

    let getMap = 
        let parseLine s = 
            match s with
            | Regex @"(.+)-(.+)" [ from; into ] -> (from, into)
            | _                                 -> failwith "nope"
        let add m (a, b) = addList a b (addList b a m)
        getDataAsStringList (Spec2021.withPath "Day12Input.txt") |> List.map parseLine |> List.fold add Map.empty |> Map.add "end" []

    let rec visit visitsMap maxVisitsMap map node =
        let smallCave (s : string) = s.ToLower() = s
        if getCount node visitsMap = getCount node maxVisitsMap && smallCave node
        then [[node]]
        else let exits = Map.find node map
             if List.length exits = 0 
             then [[node]]
             else let newVisitsMap = incCount node visitsMap
                  let rest = List.map (visit newVisitsMap maxVisitsMap map) exits |> List.flatten
                  List.map (fun restPaths -> node :: restPaths) rest

    let getSmallCavesOtherThanStartOrEnd map = 
        map |> Map.toList |> List.map fst |> List.filter (fun s -> s <> "start" && s <> "end" && s.ToLower() = s)

    let Part1 () = 
        let map = getMap
        let smallCaves = getSmallCavesOtherThanStartOrEnd map
        let maxVisitsMap = smallCaves |> List.map (fun cave -> (cave, 1)) |> Map |> incCount "start"
        visit Map.empty maxVisitsMap getMap "start" |> List.filter (List.contains "end") |> List.length

    let Part2 () = 
        let map = getMap
        let smallCaves = getSmallCavesOtherThanStartOrEnd map
        let maxVisitsMap caveWithTwoVisits = 
            smallCaves |> List.map (fun cave -> (cave, if cave = caveWithTwoVisits then 2 else 1)) |> Map |> incCount "start"
        smallCaves 
        |> List.map (fun c -> visit Map.empty (maxVisitsMap c) getMap "start" |> List.filter (List.contains "end")) 
        |> List.flatten 
        |> Set
        |> Set.count
