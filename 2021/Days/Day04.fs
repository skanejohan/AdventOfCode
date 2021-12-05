namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Regex
open Lib.Extensions.ListExtensions
open Lib.Extensions.StringExtensions
open Lib.Matrix.Matrix
open Spec2021

module Day04 =

    let parseBingoBoards lines =
        let stringToRow s =
            match s with
            | Regex @"(\d+) +(\d+) +(\d+) +(\d+) +(\d+)" cs -> List.map (fun c -> (int c, false)) cs
            | _                                             -> []
        List.split 6 lines |> List.map List.tail |> List.map (List.map stringToRow) |> List.map Matrix

    let apply n m = Matrix.map (fun (i, b) -> if i = n then (i, true) else (i, b)) m

    let bingo m = List.exists (fun xs -> List.forall snd xs) (List.append (Matrix.rows m) (Matrix.cols m)) 

    let unmarkedSum (m : Matrix<(int * bool)>) = Matrix.rows m |> List.flatten |> List.filter (fun (i, b) -> not b) |> List.map fst |> List.sum

    let readData = 
        let input = getDataAsStringList (withPath "Day04Input.txt")
        (parseBingoBoards (List.tail input), (List.head input).toInts ',')

    let Part1 () =
        let rec play (bs, ns) =
            let (bs', ns') = (List.map (apply (List.head ns)) bs, List.tail ns)
            match List.tryFind bingo bs' with
            | Some b -> List.head ns * unmarkedSum b
            | None   -> play (bs', ns')
        play readData

    let Part2 () = 
        let rec play (bs, ns) lastScore =
            if List.length ns = 0 
            then lastScore
            else
                let (bs', ns') = (List.map (apply (List.head ns)) bs, List.tail ns)
                match List.tryFind bingo bs' with
                | Some b -> let score = List.head ns * unmarkedSum b
                            if List.length bs' = 1
                            then score 
                            else let remainingBoards = List.filter (fun b -> not (bingo b)) bs' 
                                 play (remainingBoards, ns') score 
                | None   -> play (bs', ns') lastScore
        play readData 0
