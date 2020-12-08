namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day08 =

    let rec run line visited ack (data : string []) =
        if Set.contains line visited
        then (ack, false)
        else if line >= Array.length data
             then (ack, true)
             else 
                 let newVisited = Set.add line visited
                 let (newline, newack) = match data.[line] with
                                         | Prefix "acc +" rest -> (line + 1, ack + int rest)
                                         | Prefix "acc -" rest -> (line + 1, ack - int rest)
                                         | Prefix "jmp +" rest -> (line + int rest, ack)
                                         | Prefix "jmp -" rest -> (line - int rest, ack)
                                         | Prefix "nop" rest   -> (line + 1, ack)
                                         | _                   -> failwith "unknown input"
                 run newline newVisited newack data

    let Part1 _ = GetDataAsStringArray "Day08Input.txt" |> run 0 Set.empty 0 |> fst

    let jmp2nop (s : string) = s.Replace ("jmp", "nop")

    let nop2jmp (s : string) = s.Replace ("nop", "jmp")

    let replaceAt fn data idx =
        let a = Array.copy data
        let orig = a.[idx]
        Array.set a idx (fn a.[idx])
        (a.[idx] <> orig, a)

    let allPrograms fn prog = 
        seq { 0..Array.length prog-1 } |> 
        Seq.map (replaceAt fn prog) |>
        Seq.filter fst |>
        Seq.map snd

    let Part2 _ = 
        let program = GetDataAsStringArray "Day08Input.txt"
        let n2j = allPrograms nop2jmp program 
        let j2n = allPrograms jmp2nop program
        let all = Seq.concat (seq [ n2j; j2n ])
        let firstTerminating = Seq.find (fun p -> snd (run 0 Set.empty 0 p)) all 
        run 0 Set.empty 0 firstTerminating |> fst
