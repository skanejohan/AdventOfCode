namespace AdventOfCode2015

open Library
open Microsoft.FSharp.Collections

module Day01 =

    let Part1 _ =
        let contents = GetDataAsByteList "Day01Input.txt"
        let howMany pred = Seq.filter pred >> Seq.length
        let up = contents |> howMany (fun a -> a = 40uy)
        let down = contents |> howMany (fun a -> a = 41uy)
        up - down

    let Part2 _ =
        let rec fn steps level input =
            if level = -1 
            then steps
            else match input with 
                 | 40uy :: tl -> fn (steps+1) (level+1) tl
                 | _    :: tl -> fn (steps+1) (level-1) tl
                 | _          -> 0
        fn 0 0 (GetDataAsByteList "Day01Input.txt")
