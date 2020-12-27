namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day24 =
    
    let rec getDirs chars = 
        match chars with 
        | 'n' :: 'w' :: cs -> "nw" :: getDirs cs
        | 'n' :: 'e' :: cs -> "ne" :: getDirs cs
        | 's' :: 'w' :: cs -> "sw" :: getDirs cs
        | 's' :: 'e' :: cs -> "se" :: getDirs cs
        | 'w' :: cs        -> "w" :: getDirs cs
        | 'e' :: cs        -> "e" :: getDirs cs
        | _                -> []

    let isOdd x = x % 2 <> 0

    let move (x, y) dir = 
        match dir with 
        | "nw" when isOdd y -> (x, y-1)
        | "nw"              -> (x-1, y-1)
        | "ne" when isOdd y -> (x+1, y-1)
        | "ne"              -> (x, y-1)
        | "sw" when isOdd y -> (x, y+1)
        | "sw"              -> (x-1, y+1)
        | "se" when isOdd y -> (x+1, y+1)
        | "se"              -> (x, y+1)
        | "e"               -> (x+1, y)
        | _                 -> (x-1, y)

    let rec calcTarget (x, y) dirs =
        match dirs with
        | d :: ds -> calcTarget (move (x, y) d) ds
        | _       -> (x, y)

    let findWithDefault map key def =
        match Map.tryFind key map with
        | Some v -> v
        | None   -> def

    let rec calcTargets map lines = 
        match lines with
        | l :: ls -> let target = calcTarget (0, 0) l
                     let oldCount = findWithDefault map target 0
                     calcTargets (Map.add target (oldCount+1) map) ls
        | _       -> map

    let getMap file = GetDataAsCharLists file |> List.map getDirs |> calcTargets Map.empty

    let Part1 () = getMap "Day24Input.txt" |> Map.toList |> List.map snd |> List.filter isOdd |> List.length

    type Color = Black | White

    let getDimensions map =
        let keys = map |> Map.toList |> List.map fst
        let xs = List.map fst keys
        let ys = List.map snd keys
        (List.min xs, List.min ys, List.max xs, List.max ys)

    let allCoords (minX, minY, maxX, maxY) = List.allPairs [minX .. maxX] [minY .. maxY]

    let getColorMapFromPart1Map file = 
        let mapFromPart1 = getMap file
        let coords = allCoords (getDimensions mapFromPart1)
        let getColorFromPart1 pos =
            match Map.tryFind pos mapFromPart1 with
            | Some n -> if isOdd n then Black else White
            | None   -> White
        List.fold (fun m p -> Map.add p (getColorFromPart1 p) m) Map.empty coords

    let neighborsOf pos = ["nw"; "ne"; "e"; "se"; "sw"; "w"] |> List.map (move pos) 

    let noOfNeighboringBlackTiles pos map =
        neighborsOf pos |> List.filter (fun p -> Map.containsKey p map && Map.find p map = Black) |> List.length

    let colorAt pos map =
        match Map.tryFind pos map with
        | Some c -> c
        | None   -> White

    let resultingColor pos map =
        let nb = noOfNeighboringBlackTiles pos map
        match colorAt pos map with
        | Black when nb = 0 || nb > 2 -> White
        | Black                       -> Black
        | White when nb = 2           -> Black
        | _                           -> White

    let step map =
        let (minX, minY, maxX, maxY) = getDimensions map
        let coords = allCoords (minX-1, minY-1, maxX+1, maxY+1)
        List.fold (fun m p -> Map.add p (resultingColor p map) m) Map.empty coords

    let rec run n map =
        match n with
        | 0 -> map
        | n -> run (n-1) (step map)
    
    let noOfBlackTiles map = map |> Map.toList |> List.map snd |> List.filter (fun x -> x = Black) |> List.length

    let Part2 () = getColorMapFromPart1Map "Day24Input.txt" |> run 100 |> noOfBlackTiles
