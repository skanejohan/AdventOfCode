namespace AdventOfCode2021

open Data
open Regex
open Library

module Day04 =

    // -------- Cell --------------------------------------------------------------------

    type Cell = Cell of int * bool
    
    let cell c = Cell (int c, false)
    
    let cellApplyNumber n (Cell (i, b)) = if i = n then Cell (i, true) else Cell (i, b)

    let cellSumOfUnmarkedNumbers (Cell (i, b)) = if b then 0 else i

    // -------- Line --------------------------------------------------------------------

    type Line = Line of Cell list
    
    let parseLine lines =
        let l = List.head lines
        let ls = List.tail lines
        match l with
        | Regex @"(\d+) +(\d+) +(\d+) +(\d+) +(\d+)" l -> (Line (List.map cell l), ls)
        | _                                            -> (Line [], ls)

    let fromLine (Line cells) = cells

    let lineApplyNumber n (Line cells) = Line (List.map (cellApplyNumber n) cells)
    
    let lineHasBingo (Line cells) = List.forall (fun (Cell (i, b)) -> b) cells

    let lineSumOfUnmarkedNumbers (Line cells) = List.sumBy cellSumOfUnmarkedNumbers cells

    // -------- Board --------------------------------------------------------------------

    type Board = Board of Line list

    let parseBoard input =
        let (line1, remaining1) = parseLine (List.tail input) // List.tail to skip empty line
        let (line2, remaining2) = parseLine remaining1
        let (line3, remaining3) = parseLine remaining2
        let (line4, remaining4) = parseLine remaining3
        let (line5, remaining5) = parseLine remaining4
        (Board [line1; line2; line3; line4; line5], remaining5)
        
    let parseBoards input =
        let rec get inp acc =
            match inp with
            | s :: ss -> let (b, inp') = parseBoard inp
                         get inp' (b :: acc)
            | _       -> acc
        get input []

    let boardApplyNumber n (Board lines) = Board (List.map (lineApplyNumber n) lines)

    let boardVerticalLines lines = List.map fromLine lines |> rotateLists |> List.map Line
        
    let boardHasBingo (Board lines) = List.exists lineHasBingo lines || List.exists lineHasBingo (boardVerticalLines lines)
    
    let boardsApplyNumber n boards = List.map (boardApplyNumber n) boards

    let boardsApplyNext numbers boards = (boardsApplyNumber (List.head numbers) boards, List.tail numbers)

    let boardThatHasBingo boards = List.tryFind boardHasBingo boards

    let boardSumOfUnmarkedNumbers (Board lines) = List.sumBy lineSumOfUnmarkedNumbers lines

    let readData = 
        let input = getDataAsStringList "Day04Input.txt"
        let numbers = stringToInts (List.head input) ','
        let boards = parseBoards (List.tail input)
        (boards, numbers)

    let Part1 () =
        let rec play (boards, numbers) =
            let (b, n) = boardsApplyNext numbers boards
            match boardThatHasBingo b with
            | Some board -> List.head numbers * boardSumOfUnmarkedNumbers board
            | None       -> play (b, n)
        play readData

    let Part2 () =
        let rec play (boards, numbers) lastBingoScore =
            if List.length numbers = 0 
            then lastBingoScore
            else
                let (b, n) = boardsApplyNext numbers boards
                match boardThatHasBingo b with
                | Some board -> let score = List.head numbers * boardSumOfUnmarkedNumbers board
                                if List.length b = 1
                                then score 
                                else let remainingBoards = List.filter (fun b -> not (boardHasBingo b)) b 
                                     play (remainingBoards, n) score 
                | None       -> play (b, n) lastBingoScore
        play readData 0
