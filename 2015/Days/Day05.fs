namespace AdventOfCode2015

open Microsoft.FSharp.Collections
open Library

module Day05 =

    let gt x y = y > x

    let isVowel c = c = 'a' || c = 'e' || c = 'i' || c = 'o' || c = 'u'

    let hasThreeVowels s = String.filter isVowel s |> String.length |> gt 2

    let rec hasLetterTwice s = 
        match s with
        | c1 :: c2 :: cs -> if c1 = c2 then true else hasLetterTwice (c2 :: cs)
        | _              -> false
        
    let hasForbiddenString (s : string) = s.Contains "ab" || s.Contains "cd" || s.Contains "pq" || s.Contains "xy"

    let isNice1 s = hasThreeVowels s && hasLetterTwice (Seq.toList s) && not (hasForbiddenString s)

    let Part1 () = GetDataAsStringList "Day05Input.txt" |> List.filter isNice1 |> List.length

    let hasRepeatedPair chars =
        let rec check cs set last =
            match cs with
            | a :: b :: c :: css -> if last <> (a,b) && Set.contains (a,b) set 
                                    then true
                                    else check (b :: c :: css) (Set.add (a,b) set) (a,b)
            | a :: b :: _        -> Set.contains (a,b) set
            | _                  -> false
        check chars Set.empty (' ',' ')

    let rec hasSameLetterTwoStepsApart s = 
        match s with
        | c1 :: c2 :: c3 :: cs when c1 = c3 -> true
        | c1 :: cs                          -> hasSameLetterTwoStepsApart cs
        | _                                 -> false

    let isNice2 s = hasRepeatedPair s && hasSameLetterTwoStepsApart s

    let Part2 () = GetDataAsStringList "Day05Input.txt" |> List.map Seq.toList |> List.filter isNice2 |> List.length
