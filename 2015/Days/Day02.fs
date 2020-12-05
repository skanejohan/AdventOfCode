namespace AdventOfCode2015

open Library
open Microsoft.FSharp.Collections

module Day02 =

    let private getDimensions (line : string) =
        match line.Split 'x' |> Array.toList with
        | [x; y; z] -> Some (int x, int y, int z)
        | _         -> None

    let liftOption l = 
        if List.contains None l then None
        else Some (List.map Option.get l)

    let private requiredPaper (w, h, l) = 2 * w * h + 2 * w * l + 2 * h * l + min (w * h) (min (w * l) (h * l))

    let private requiredRibbon (w, h, l) = 
        let vol = w * h * l
        let dist1 = 2 * w + 2 * h
        let dist2 = 2 * w + 2 * l
        let dist3 = 2 * h + 2 * l
        vol + min dist1 (min dist2 dist3)

    let private calc fn =
        match List.map getDimensions (GetDataAsStringList "Day02Input.txt") |> liftOption with
        | Some dimensions -> List.sumBy fn dimensions
        | None            -> 0

    let Part1 _ = calc requiredPaper

    let Part2 _ = calc requiredRibbon
