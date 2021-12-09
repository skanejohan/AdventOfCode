namespace Lib.Extensions

module StringExtensions =

    type System.String with
        member s.toInts c = s.Split [|c|] |> Array.toList |> List.map int

        member s.intersect s2 = 
            let set1 = s |> Seq.toList |> Set.ofList
            let set2 = s2 |> Seq.toList |> Set.ofList
            Set.intersect set1 set2 |> Set.toList|> List.map string |> List.reduce (+)
