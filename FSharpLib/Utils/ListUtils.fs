namespace Lib.Utils

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
