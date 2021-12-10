namespace AdventOfCode2015

open Lib.DataLoader
open Lib.Regex

module Day06 =

    type Op = | On = 1 | Off = 2 | Toggle = 3

    let private parseLine line = 
        match line with
        | Regex @"turn on (\d*),(\d*) through (\d*),(\d*)" [ x1; y1; x2; y2 ]  -> (Op.On, int x1, int y1, int x2, int y2)
        | Regex @"turn off (\d*),(\d*) through (\d*),(\d*)" [ x1; y1; x2; y2 ] -> (Op.Off, int x1, int y1, int x2, int y2)
        | Regex @"toggle (\d*),(\d*) through (\d*),(\d*)" [ x1; y1; x2; y2 ]   -> (Op.Toggle, int x1, int y1, int x2, int y2)
        | _                                                                    -> failwith "invalid input"

    let getRules = getDataAsStringList (Spec2015.withPath "Day06Input.txt") |> List.map parseLine

    let inside x y (x1, y1, x2, y2) = x1 <= x && y1 <= y && x <= x2 && y <= y2

    let evalRule x y (b, v) (op, x1, y1, x2, y2) = 
        if inside x y (x1, y1, x2, y2) 
        then match op with
             | Op.On -> (true, v+1)
             | Op.Off -> (false, if v > 1 then v-1 else 0)
             | _ -> (not b, v+2)
        else (b, v)

    let evalRules rules (x, y) =
        let rec ev rules' (b, v) = match rules' with
                                   | r :: rs -> ev rs (evalRule x y (b, v) r)
                                   | _       -> (b, v)
        ev rules (false, 0)

    let allCoords = seq { for y in 0..999 do yield! seq { for x in 0..999 -> (x, y) } }
    
    let Part1 () = allCoords |> Seq.map (evalRules getRules) |> Seq.map fst |> Seq.filter id |> Seq.length
    
    let Part2 () = allCoords |> Seq.map (evalRules getRules) |> Seq.map snd |> Seq.sum
