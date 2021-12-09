namespace Lib.Utils

module StringUtils =

    let charArrayToString (cs : char []) = cs |> Array.map string |> Array.reduce (+) 

    let charListToString (cs : char list) = cs |> List.map string |> List.reduce (+) 

    let stringToCharList (s : string) = Seq.toList s

    let stringToCharArray (s : string) = Seq.toArray s

    let withCharsInOrder (s : string) = s |> stringToCharList |> List.sort |> charListToString