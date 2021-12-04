namespace AdventOfCode2018

open Library

module Day01 =

    let Part1 () = GetDataAsInts "Day01Input.txt" |> List.sum

    let Part2 () =
        let values = GetDataAsIntArray "Day01Input.txt" 
        let next i = values.[i % Array.length values]
        let rec collision set i f =
            let newF = f + next i
            if Set.contains newF set then newF else collision (Set.add newF set) (i+1) newF
        collision Set.empty 0 0
    