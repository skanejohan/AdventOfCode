namespace AdventOfCode2021

open IntList
open BitList
open Data

module Day03 =

    let Part1 () = 
        let input = getDataAsBitLists "Day03Input.txt"
        let result = List.map (fun i -> if i > (List.length input / 2) then 1 else 0) (fromIntList (sumBitLists input)) |> BitList
        bitListToInt64 result * bitListToInt64 (bitListInvert result)

    let mostCommonAt bitPos values =
        if List.length (bitListsFilteredByBitSet bitPos values) >= List.length (bitListsFilteredByBitClear bitPos values) 
        then 1 
        else 0

    let leastCommonAt bitPos values = 1 - mostCommonAt bitPos values

    let Part2 () =
        let calcIt (input : BitList list) (fn : int -> BitList list -> int) = 
            let rec calc pos inp =
                if List.length inp = 1
                then List.head inp
                else if fn pos inp = 1
                     then let inp' = bitListsFilteredByBitSet pos inp
                          calc (pos + 1) inp'
                     else let inp' = bitListsFilteredByBitClear pos inp
                          calc (pos + 1) inp'
            calc 0 input
        let calcOxygen input = calcIt input mostCommonAt
        let calcCo2 input = calcIt input leastCommonAt
        let input = getDataAsBitLists "Day03Input.txt"
        bitListToInt64 (calcOxygen input) * bitListToInt64 (calcCo2 input)
