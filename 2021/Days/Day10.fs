namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Utils.ListUtils

module Day10 =

    let openingChars = ['('; '['; '{'; '<' ]
    let closingChars = [')'; ']'; '}'; '>' ]
    let isOpening o = List.contains o openingChars
    let isClosing c = List.contains c closingChars
    let matches o c = List.findIndex ((=) o) openingChars = List.findIndex ((=) c) closingChars
    let interpret s =
        let rec find s' stack =
            match s' with
            | c :: cs -> if isOpening c 
                         then find cs (c :: stack)
                         else if isClosing c && matches (List.head stack) c
                              then find cs (List.tail stack)
                              else if isClosing c
                              then Error c
                              else find cs stack
            | _       -> Ok stack
        find s []
            
    let Part1 () = 
        let points = Map.empty |> Map.add ')' 3 |> Map.add ']' 57 |> Map.add '}' 1197 |> Map.add '>' 25137
        let data = getDataAsCharLists (Spec2021.withPath "Day10Input.txt") |> List.map interpret
        data |> getErrors |> List.map (fun k -> Map.find k points) |> List.sum

    let Part2 () =
        let points = Map.empty |> Map.add ')' 1L |> Map.add ']' 2L |> Map.add '}' 3L |> Map.add '>' 4L
        let score characters =
            let rec sc chars n =
                match chars with
                | c :: cs -> sc cs (n * 5L + (Map.find c points))
                | _       -> n
            sc characters 0L
        let matching = Map.empty |> Map.add '(' ')' |> Map.add '[' ']' |> Map.add '{' '}' |> Map.add '<' '>'
        let data = getDataAsCharLists (Spec2021.withPath "Day10Input.txt") |> List.map interpret
        let scores = data |> getOks |> List.map (fun l -> List.map (fun c -> Map.find c matching) l) |> List.map score |> List.sort |> List.toArray
        scores.[(Array.length scores - 1) / 2]
