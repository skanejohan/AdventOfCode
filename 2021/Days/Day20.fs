namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Utils.BoolListUtils
open Lib.Utils.MapUtils
open Lib.Utils.TwodUtils

module Day20 =
    
    let getIndexAt x y map background = areaAround (x, y) |> List.map (get map background) |> boolsToInt

    let getBounds map = 
        let xs = map |> Map.toList |> List.map (fun ((x, _), _) -> x)
        let ys = map |> Map.toList |> List.map (fun ((_, y), _) -> y)
        (List.min xs, List.min ys, List.max xs, List.max ys)

    let step (algo : bool []) background map = 
        let (minX, minY, maxX, maxY) = getBounds map
        [for x in minX-1..maxX+1 do for y in minY-1..maxY+1 do yield ((x, y), algo.[getIndexAt x y map background])] |> Map

    let rec stepN (algo : bool []) n backgroundFn map = 
        if n = 0 then map else stepN algo (n-1) backgroundFn (step algo (backgroundFn n) map)

    let dumpMap map = 
        let (minX, minY, maxX, maxY) = getBounds map
        for y in minY..maxY do
            for x in minX..maxX do
                let c = if get map false (x, y) then '#' else '.'
                printf "%c" c
            printfn ""

    let countLit map = map |> Map.toList |> posListFilter id |> List.length

    let setup fileName = 
        let isHash c = c = '#' 
        let input = getDataAsCharLists (Spec2021.withPath fileName)
        let algo = List.head input |> List.map isHash |> List.toArray
        let backgroundFn = if algo.[0] then (fun i -> i % 2 <> 0) else (fun i -> false)
        let image = List.skip 2 input |> listListToPosList |> posListMap isHash |> Map
        (image, algo, backgroundFn)

    let Part1 () = 
        let (image, algo, backgroundFn) = setup "Day20Input.txt"
        stepN algo 2 backgroundFn image|> countLit

    let Part2 () = 
        let (image, algo, backgroundFn) = setup "Day20Input.txt"
        stepN algo 50 backgroundFn image|> countLit
