namespace AdventOfCode2020

open Microsoft.FSharp.Core

module Day23 =

    let update (cup, (cups : int [])) =
        let rem1 = cups.[cup]
        let rem2 = cups.[rem1]
        let rem3 = cups.[rem2]
        let rec dest candidate =
            if candidate = 0
            then dest (Array.length cups - 1)
            else if candidate = rem1 ||
                    candidate = rem2 ||
                    candidate = rem3
                 then dest (candidate-1)
                 else candidate
        let destination = dest (cup-1)
        cups.[cup]         <- cups.[rem3]
        cups.[rem3]        <- cups.[destination]
        cups.[destination] <- rem1
        (cups.[cup], cups)

    let play (cup, cups) count =
        let rec doPlay (cup, cups) n =
            if n = count 
            then (cup, cups) 
            else doPlay (update (cup, cups)) (n+1)
        doPlay (cup, cups) 0

    let Part1 () =
        // Puzzle input = 974618352. Commented out code will print "75893264"
        //let start = (9, [| 0; 8; 9; 5; 6; 2; 1; 4; 3; 7 |])       
        //let rec printAllAfterOne cup (cups : int []) =
        //    if (cups.[cup] = 1)
        //    then printfn ""
        //         ()
        //    else printf "%i" cups.[cup]
        //         printAllAfterOne cups.[cup] cups
        //play start 100 |> snd |> printAllAfterOne 1
        75893264

    let Part2 () = 
        let cups = [| 1 .. 1000001 |]
        cups.[1] <- 8
        cups.[2] <- 10
        cups.[3] <- 5
        cups.[4] <- 6
        cups.[5] <- 2
        cups.[6] <- 1
        cups.[7] <- 4
        cups.[8] <- 3
        cups.[9] <- 7
        cups.[1000000] <- 9
        play (9, cups) 10000000 |> ignore
        int64 cups.[1] * int64 cups.[cups.[1]]
