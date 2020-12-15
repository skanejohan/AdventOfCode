namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day15 =

    let say (num,count,spoken) = 
        let next = match Map.tryFind num spoken with
                   | Some value -> count - value
                   | None       -> 0
        (next, count+1, Map.add num count spoken)

    let rec play target (num,count,spoken) =
        if count = target
        then num
        else say (num, count, spoken) |> play target

    let getInitialConfiguration _ =
        let (_, c1, s1) = say (18, 0, Map.empty)
        let (_, c2, s2) = say (11, c1, s1)
        let (_, c3, s3) = say (9, c2, s2)
        let (_, c4, s4) = say (0, c3, s3)
        let (_, c5, s5) = say (5, c4, s4)
        say (1, c5, s5)

    let Part1 _ = play 2019 (getInitialConfiguration 0)
    
    let Part2 _ = play 29999999 (getInitialConfiguration 0)
