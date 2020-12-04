namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections

module Day03 =

    let blocks x y (area : char [] []) = area.[y].[x] = '#'

    let rec slope x y dx dy w h = if (y < h) then (x % w, y) :: (slope (x+dx) (y+dy) dx dy w h) else []

    let calcCollissions dx dy =
        let area = GetDataAsCharArrays "Day03Input.txt"
        let h = Array.length area
        let w = Array.length area.[0]
        let sl = slope 0 0 dx dy w h
        List.filter (fun (x, y) -> blocks x y area) sl |> List.length

    let Part1 _ = calcCollissions 3 1

    let Part2 _ = (calcCollissions 1 1) * (calcCollissions 3 1) * (calcCollissions 5 1) * (calcCollissions 7 1) * (calcCollissions 1 2)
