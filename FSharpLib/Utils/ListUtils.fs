namespace Lib.Utils

open Lib.Utils.CharUtils

module ListUtils =

    let elemInList list elem = List.contains elem list

    let elemNotInList list elem = List.contains elem list |> not

    let elemInAllLists lists elem = List.forall (fun list -> elemInList list elem) lists

    let elemInNoOfLists no lists elem = (List.map (fun list -> elemInList list elem) lists |> List.filter id |> List.length) = no

    let rec getSomes list = 
        match list with 
        | x :: xs -> match x with
                     | Some a -> a :: getSomes xs
                     | None   -> getSomes xs
        | _       -> []

    let rec getOks list = 
        match list with 
        | x :: xs -> match x with
                     | Ok a    -> a :: getOks xs
                     | Error _ -> getOks xs
        | _       -> []

    let rec getErrors list = 
        match list with 
        | x :: xs -> match x with
                     | Ok _    -> getErrors xs
                     | Error e -> e :: getErrors xs
        | _       -> []

    // cartesian [0; 1; 3] [0; 2; 4] -> [(0, 0); (0, 2); (0, 4); (1, 0); (1, 2); (1, 4); (3, 0); (3, 2); (3, 4)]
    let cartesian xs ys = xs |> List.collect (fun x -> ys |> List.map (fun y -> x, y))

    let cartesianMap xs ys fn = cartesian xs ys |> List.map fn

    let intListToString is = List.map numToChar is |> List.map string |> List.reduce (+)
