namespace AdventOfCode2021

open Lib.Extensions.StringExtensions
open Lib.DataLoader
open Spec2021

module Day07 =

    let calcMinCons moveCost = 
        let input = (getDataAsString (withPath "Day07Input.txt")).toInts ','
        let cons l n = List.sumBy (moveCost n) l
        List.map (cons input) [List.min input..List.max input] |> List.min

    let Part1 () = 
        let moveCost a b = abs (a - b)
        calcMinCons moveCost

    let Part2 () = 
        let moveCost a b = let dist = abs (a - b)
                           (dist * (dist + 1)) / 2
        calcMinCons moveCost
