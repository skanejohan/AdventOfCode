namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Regex
open Lib.Geometry.Box

module Day22 = 
    let private parseLine line = 
        match line with
        | Regex @"on x=([^.]*)..([^,]*),y=([^.]*)..([^,]*),z=([^.]*)..(.*)" [ x1; x2; y1; y2; z1; z2]  -> (true, int x1, int x2, int y1, int y2, int z1, int z2)
        | Regex @"off x=([^.]*)..([^,]*),y=([^.]*)..([^,]*),z=([^.]*)..(.*)" [ x1; x2; y1; y2; z1; z2] -> (false, int x1, int x2, int y1, int y2, int z1, int z2)
        | _                                                                    -> failwith "invalid input"

    let rules = getDataAsStringList (Spec2021.withPath "Day22Input.txt") |> List.map parseLine

    let inside x y z (x1, x2, y1, y2, z1, z2) = x1 <= x && x <= x2 && y1 <= y && y <= y2 && z1 <= z && z <= z2

    let evalRule on (x, y, z) (op, x1, x2, y1, y2, z1, z2) = 
        if inside x y z (x1, x2, y1, y2, z1, z2) 
        then if op 
             then Set.add (x, y, z) on
             else Set.remove (x, y, z) on
        else on

    let evalRules on pos =
        let rec ev rules' on' = match rules' with
                                | r :: rs -> ev rs (evalRule on' pos r)
                                | _       -> on'
        ev rules on

    let allCoords = [ for z in -50..50 do for y in -50..50 do for x in -50..50 do (x, y, z) ]

    let Part1 () = List.fold evalRules Set.empty allCoords |> Set.count

    // As expected, the naïve implementation doesn't work for part 2 - use the Box type.

    let Part2 () = 
        let cubes = rules |> List.map (fun (b, x1, x2, y1, y2, z1, z2) -> (b, (x1, x2, y1, y2, z1, z2)))
        let rec countLitCubes cubes =
            match cubes with
            | (false, _) :: cs  -> countLitCubes cs
            | (true, box) :: cs -> let remainingBoxes = cs |> List.map (fun (_, b) -> b)
                                   let overlaps = Box.totalOverlaps box remainingBoxes
                                   Box.volume box + countLitCubes cs - Box.totalVolume overlaps
            | _                 -> bigint 0
        countLitCubes cubes
