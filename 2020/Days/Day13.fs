namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day13 =

    let Part1 () =
        let busLeavingAt buses t = List.tryFind (fun b -> t % b = 0L) buses
        let rec findBusAndTime buses t = 
            match busLeavingAt buses t with
            | Some b -> (b, t)
            | None   -> findBusAndTime buses (t+1L)
        let (b, t) = findBusAndTime [23L; 37L; 863L; 19L; 13L; 17L; 29L; 571L; 41L] 1000052L
        b * (t - 1000052L)

    let Part2 () =
        let rec processBus time inc (index, bus) =
            let newTime = time + inc
            match (newTime + index) % bus with 
            | 0L -> (newTime, inc * bus)
            | _  -> processBus newTime inc (index, bus)
        let rec processBuses buses (t, inc) =
            match buses with
            | b :: bs -> processBuses bs (processBus t inc b)
            | _       -> t
        let buses = [(0L, 23L); (17L, 37L); (23L, 863L); (35L, 19L); (36L, 13L); (40L, 17L); (52L, 29L); (54L, 571L); (95L, 41L)]
        processBuses (List.skip 1 buses) (0L, snd buses.[0]) 
