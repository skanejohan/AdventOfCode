namespace AdventOfCode2015

open Microsoft.FSharp.Collections
open System.Security.Cryptography
open System

module Day04 =

    let rec inputs n = seq {
        yield "yzbqklnj" + string n
        yield! inputs (n+1) }

    let hash (s : string) = s |> System.Text.Encoding.ASCII.GetBytes |> MD5.Create().ComputeHash 
                              |> Seq.map (fun c -> c.ToString("x2")) |> Seq.reduce (+)
                               
    let stringOfZeros n = String.init n (fun n -> "0")

    let hashStartsWith start (s : string) = 
        let h = hash s
        h.StartsWith start

    let firstValid n s = 
        let zeros = stringOfZeros n
        Seq.find (hashStartsWith zeros) s

    let solve n = inputs 1 |> firstValid n |> Seq.skipWhile Char.IsLetter  |> Seq.map string |> Seq.toArray |> String.concat "" |> int

    let Part1 _ = solve 5

    let Part2 _ = solve 6

