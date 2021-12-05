namespace Lib.Extensions

module StringExtensions =

    type System.String with
        member s.toInts c = s.Split [|c|] |> Array.toList |> List.map int

