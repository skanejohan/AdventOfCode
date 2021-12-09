namespace Lib.Utils

module FileUtils =

    let elemInList list elem = List.contains elem list

    let elemNotInList list elem = List.contains elem list |> not

    let elemInAllLists lists elem = List.forall (fun list -> elemInList list elem) lists

    let elemInNoOfLists no lists elem = (List.map (fun list -> elemInList list elem) lists |> List.filter id |> List.length) = no
