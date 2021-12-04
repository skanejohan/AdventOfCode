namespace AdventOfCode2021

open Lib.BitList
open Lib.BitLists
open Lib.DataLoader
open Spec2021

module Day03 =

    let Part1 () = 
        let input = getDataAsBitLists (withPath "Day03Input.txt")
        let result = List.map (fun i -> if i > (BitLists.length input / 2) then 1 else 0) (BitLists.sum input) |> BitList
        BitList.toInt64 result * BitList.toInt64 (BitList.invert result)

    let mostCommonAt bitPos values =
        if BitLists.length (BitLists.byBitSet bitPos values) >= BitLists.length (BitLists.byBitClear bitPos values) 
        then 1 
        else 0

    let leastCommonAt bitPos values = 1 - mostCommonAt bitPos values

    let Part2 () =
        let calcIt (input : BitLists) (fn : int -> BitLists -> int) = 
            let rec calc pos inp =
                if BitLists.length inp = 1
                then BitLists.head inp
                else if fn pos inp = 1
                     then let inp' = BitLists.byBitSet pos inp
                          calc (pos + 1) inp'
                     else let inp' = BitLists.byBitClear pos inp
                          calc (pos + 1) inp'
            calc 0 input
        let calcOxygen input = calcIt input mostCommonAt
        let calcCo2 input = calcIt input leastCommonAt
        let input = getDataAsBitLists (withPath "Day03Input.txt")
        BitList.toInt64 (calcOxygen input) * BitList.toInt64 (calcCo2 input)
