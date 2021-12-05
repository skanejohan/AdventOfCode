namespace AdventOfCode2021

open FSharp.Collections
open Lib.DataLoader
open Lib.Regex
open Lib.Collections.CountMap
open Lib.Extensions.ListExtensions
open Lib.Geometry.Point
open Lib.Geometry.Line
open Spec2021

module Day05 =

    let stringToLine s = 
        match s with
        | Regex @"(\d+),(\d+) -> (\d+),(\d+)" [a; b; c; d] -> Line.create (int a) (int b) (int c) (int d)
        | _                                                -> Line.empty

    let processData filterFunction = 
        getDataAsStringList (withPath "Day05Input.txt")
        |> List.map stringToLine 
        |> List.filter filterFunction
        |> List.map Line.allPoints
        |> List.flatten
        |> CountMap.createFrom<Point>
        |> CountMap.get 
        |> Map.toList 
        |> List.map snd 
        |> List.filter (fun i -> i > 1) 
        |> List.length

    let Part1 () = processData Line.isHorizontalOrVertical

    let Part2 () = processData (fun _ -> true)
