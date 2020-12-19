namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core
open FParsec
open Day18Parser

module Day18 =

    let interpret parser code = 
        match run parser code with
        | Success (result, _, _) -> result
        | Failure (msg, _, _)    -> printfn "%s" msg
                                    Num -1L

    let rec evaluate expr =
        match expr with
        | Add (e1, e2) -> (evaluate e1) + (evaluate e2)
        | Mul (e1, e2) -> (evaluate e1) * (evaluate e2)
        | Num e -> e

    let removeSpaces s = String.filter (fun c -> c <> ' ') s

    let solve parser = GetDataAsStringList "Day18Input.txt" |> 
                       List.map removeSpaces |> 
                       List.map (interpret parser) |> 
                       List.map evaluate |>
                       List.sum
    
    let Part1 () = solve pExpr1
    
    let Part2 () = solve pExpr2
