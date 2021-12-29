namespace AdventOfCode2021

open Lib.Utils.ListUtils

module Day24 =

    // (xAddend, yAddend, zDivisor, taken from input)
    let coefficients = [ (14, 12, 1); (10, 9, 1); (13, 8, 1); (-8, 3, 26); 
                         (11, 0, 1); (11, 11, 1); (14, 10, 1); (-11, 13, 26); 
                         (14, 3, 1); (-1, 10, 26); (-8, 10, 26); (-5, 14, 26); 
                         (-16, 6, 26); (-6, 5, 26) ]

    // Given that I want the resulting z out from the given block, what are the possible input z values assuming
    // fixed w? Returns a list of pairs where each pair representent the path taken so far and an input z value.
    // findPossibleZsForW :: int list -> int -> int -> int -> (int list * int) list
    let findPossibleInputZsForGivenW path block desiredZ w =
        let newpath = w :: path
        let (xAdd, yAdd, zDiv) = coefficients.[block]
        let x = desiredZ - w - yAdd
        let result = if x % 26 = 0 then [(newpath, x / 26 * zDiv)] else []
        if 0 <= w-xAdd && w-xAdd < 26 
        then (newpath, w - xAdd + desiredZ * zDiv) :: result
        else result

    let getAllAcceptedModelNumbers () = 
        let findPossibleInputZsForAllValidW path n desiredZ = 
            List.map (findPossibleInputZsForGivenW path n desiredZ) [1..9] |> List.concat
        let find n list = 
            List.map (fun (path, z) -> findPossibleInputZsForAllValidW path n z) list
        let rec getRec n ack =
            if n = -1
            then ack
            else getRec (n-1) (find n ack |> List.concat)
        getRec 13 [([], 0)] |> List.filter (fun (a, b) -> b = 0) |> List.map (fun (a, b) -> int64 (intListToString a))
    
    let Part1 () = getAllAcceptedModelNumbers () |> List.max

    let Part2 () = getAllAcceptedModelNumbers () |> List.min
