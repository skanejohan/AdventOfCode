namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections

module Day06 =

    // Get a list of lists of strings where each sub-list represents a group's answers, e.g. 
    // ["arke"; "qzr"; "plmgnr"; "uriq"] for the second group in Day06Input.txt.
    let getGroups data = data |> splitBy (fun s -> String.length s = 0) 

    // Get a list of strings where each string represents a group's answers, e.g. 
    // "arkeqzrplmgnruriq" for the second group in Day06Input.txt.
    let getGroupAnswers groups = groups |> Seq.map (fun ss -> String.concat "" ss)

    // Given a list of strings, returns a set of the characters that appear in all the strings
    let inAllStrings (ss : seq<string>) = 
        let sets = ss |> Seq.map Seq.toList |> Seq.map Set.ofList
        Seq.fold Set.intersect (Set.ofSeq (Seq.head sets)) sets

    let Part1 _ = 
        GetDataAsStringList "Day06Input.txt" |> getGroups |> getGroupAnswers |>
        Seq.map (fun s -> Set.ofSeq (Seq.toArray s)) |> Seq.map Set.count |> Seq.sum 

    let Part2 _ = 
        GetDataAsStringList "Day06Input.txt" |> getGroups |>
        Seq.map (Seq.filter (fun (s : string) -> s.Length > 0) ) |>
        Seq.map inAllStrings |> Seq.toList |>
        List.sumBy Set.count
