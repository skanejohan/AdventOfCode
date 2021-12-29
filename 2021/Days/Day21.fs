namespace AdventOfCode2021

open Lib.Utils.MapUtils
open Lib.Utils.ListUtils

module Day21 =

    let wrap i n = (i-1) % n + 1
    let wrapPos i = wrap i 10
    let wrapDie i = wrap i 100

    let step state player = 
        let (p1p, p1s, p2p, p2s, rolled, latestRoll) = state
        let roll = latestRoll * 3 + 6
        if player = 1
        then let p1pp = wrapPos (p1p + roll)
             let p1ss = p1s + p1pp
             (p1pp, p1ss, p2p, p2s, rolled + 3, wrapDie (latestRoll + 3))
        else let p2pp = wrapPos (p2p + roll)
             let p2ss = p2s + p2pp
             (p1p, p1s, p2pp, p2ss, rolled + 3, wrapDie (latestRoll + 3))

    let play state = 
        let rec p state n =
            let (_, p1s, _, p2s, rolled, _) = state
            if p1s >= 1000 
            then p2s * rolled
            else if p2s >= 1000
                 then p1s * rolled
                 else let newState = step state (wrap n 2)
                      p newState (n+1)
        p state 1

    let Part1 () = play (10, 0, 4, 0, 0, 0)

    let Part2 () = 
        let outcomes = [(3, 1I); (4, 3I); (5, 6I); (6, 7I); (7, 6I); (8, 3I); (9, 1I)]

        let universes = [((10, 4, 0, 0, None), 1I)]

        let apply n (((p1p, p2p, p1s, p2s, (winner : int option)), uc), (roll, oc)) = 
            if winner.IsSome
            then ((p1p, p2p, p1s, p2s, winner), uc)
            else if n = 1 
                 then let p = wrapPos (p1p + roll)
                      let s = p1s + p
                      let w = if winner.IsNone && s >= 21 then Some 1 else winner
                      ((p, p2p, s, p2s, w), uc * oc)
                 else let p = wrapPos (p2p + roll)
                      let s = p2s + p
                      let w = if winner.IsNone && s >= 21 then Some 2 else winner
                      ((p1p, p, p1s, s, w), uc * oc)

        let withWinner n universes =
            let result = List.groupBy (fun ((_, _, _, _, (w : int option)), _) -> w.IsSome && w.Value = n) universes
            let newUniverses = let universeList = List.filter (fun (b, _) -> not b) result 
                               match universeList with
                               | [] -> []
                               | _  -> universeList |> List.map snd |> List.head
            let winCount = let winCountList = List.filter (fun (b, _) -> b) result |> List.map snd
                           match winCountList with
                           | [] -> 0I
                           | _  -> winCountList |> List.head |> List.map snd |> List.sum
            (winCount, newUniverses)

        let stepOne (winCount1, winCount2, universes0) = 
            let mergeUniverses us = List.fold (fun m (u, c) -> addCountB u c m) Map.empty us |> Map.toList
            let (wins1, universes1) = cartesianMap universes0 outcomes (apply 1) |> mergeUniverses |> withWinner 1
            let (wins2, universes2) = cartesianMap universes1 outcomes (apply 2) |> mergeUniverses |> withWinner 2
            (winCount1 + wins1, winCount2 + wins2, universes2)

        let playUntilDone us =
            let rec play (a, b, c) =
                if List.length c = 0 then (a, b) else play (stepOne (a, b, c))
            play (0I, 0I, us)

        let (a, b) = playUntilDone universes
        max a b
