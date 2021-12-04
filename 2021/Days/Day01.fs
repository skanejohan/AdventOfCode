namespace AdventOfCode2021

open Lib.DataLoader
open Spec2021

module Day01 =

    let rec incs x xs acc =
        match xs with
        | b :: bs when b > x -> incs b bs (acc+1)
        | b :: bs            -> incs b bs acc
        | _                  -> acc

    let Part1 () = 
        let input = getDataAsInts (withPath "Day01Input.txt")
        incs (List.head input) (List.tail input) 0

    let Part2 () = 
        let rec win xs =
            match xs with
            | x1 :: x2 :: x3 :: xss -> (x1 + x2 + x3) :: win (x2 :: x3 :: xss)
            | _                     -> []
        let input = getDataAsInts (withPath "Day01Input.txt") |> win 
        incs (List.head input) (List.tail input) 0