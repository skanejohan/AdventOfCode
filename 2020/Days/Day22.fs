namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day22 =

    let score a =
        let rec sc list ack v =
            match list with
            | l :: ls -> sc ls (ack + (int64 l) * v) (v + 1L)
            | _       -> ack
        sc (List.rev a) 0L 1L

    let aWins a b = if List.length b = 0
                    then (List.tail a @ [List.head a], []) 
                    else (List.tail a @ [List.head a; List.head b], List.tail b)

    let bWins a b = if List.length a = 0
                    then ([], List.tail b @ [List.head b]) 
                    else (List.tail a, List.tail b @ [List.head b; List.head a]) 

    let aCards () = [28;3;35;27;19;40;14;15;17;22;45;47;26;13;32;38;43;24;29;5;31;48;49;41;25]

    let bCards () = [34;12;2;50;16;1;44;11;36;6;10;42;20;8;46;9;37;4;7;18;23;39;30;33;21]

    let Part1 () = 
        let playCombatRound (a, b) =
            if List.head a > List.head b
            then aWins a b  
            else bWins a b
        let rec playCombat (a, b) =
            if List.length a = 0
            then score b
            else if List.length b = 0
                 then score a
                 else playCombatRound (a, b) |> playCombat
        playCombat (aCards (), bCards ())

    let Part2 () =
        let rec play hand1 hand2 prevHands =
            let newPrevHands = Set.add (hand1, hand2) prevHands
            match (hand1, hand2) with
            | hands when Set.contains hands prevHands -> (1, hand1)
            | ([], hand)                              -> (2, hand)
            | (hand, [])                              -> (1, hand)
            | (h1::tl1, h2::tl2) when h1 <= tl1.Length && h2 <= tl2.Length -> let (p, _) = play (List.take h1 tl1) (List.take h2 tl2) newPrevHands
                                                                              if p = 1 
                                                                              then play (tl1 @ [h1;h2]) tl2 newPrevHands
                                                                              else play tl1 (tl2 @ [h2;h1]) newPrevHands
            | (h1::tl1, h2::tl2) when h1 > h2                              -> play (tl1 @ [h1;h2]) tl2 newPrevHands
            | (h1::tl1, h2::tl2) when h2 > h1                              -> play tl1 (tl2 @ [h2;h1]) newPrevHands
            | _                                                            -> failwith ""
        play (aCards ()) (bCards ()) Set.empty |> fun (_, hand) -> score hand
