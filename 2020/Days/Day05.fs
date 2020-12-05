namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections

module Day05 =

    let calc u (min, len) c =
        let half = int (len / 2)
        if c = u then (min + half, half) else (min, half)

    let calcRow data = data |> List.take 7 |> List.fold (calc 'B') (0, 128) |> fst
    
    let calcCol data = data |> List.skip 7 |> List.fold (calc 'R') (0, 8) |> fst

    let calcSeat data = calcRow data * 8 + calcCol data

    let getAllSeats data = List.map calcSeat data

    let isMySeat s n = Set.contains (n-1) s && not (Set.contains n s) && Set.contains (n+1) s

    let Part1 _ = GetDataAsCharLists("Day05input.txt") |> getAllSeats |> List.max

    let Part2 _ = 
        let allSeats = GetDataAsCharLists("Day05input.txt") |> getAllSeats |> Set.ofList
        List.find (isMySeat allSeats) [1..1000]
        