namespace AdventOfCode2021

open Lib.DataLoader
open Lib.Extensions.ListExtensions
open Lib.Utils.FileUtils
open Lib.Utils.StringUtils

module Day08 =

    let loadInput = 
        getDataAsStringList (Spec2021.withPath "Day08Input.txt")
        |> List.map (fun s -> let parts = s.Split [|'|'|]
                              let sp = parts.[0].Split [|' '|] |> Array.toList |> List.filter (fun s -> s <> "")
                              let ov = parts.[1].Split [|' '|] |> Array.toList |> List.filter (fun s -> s <> "")
                              (sp, ov))

    let Part1 () = 
        let ovLens = loadInput |> List.map snd |> List.flatten |> List.map String.length
        let one = List.filter ((=) 2) ovLens |> List.length
        let four = List.filter ((=) 4) ovLens |> List.length
        let seven = List.filter ((=) 3) ovLens |> List.length
        let eight = List.filter ((=) 7) ovLens |> List.length
        one + four + seven + eight

    let id x = x

    let calc signals (outputs : string list) =
        let ofLen n = List.filter (fun s -> String.length s = n)        
        let repr1 = signals |> ofLen 2 |> List.head |> stringToCharList
        let repr4 = signals |> ofLen 4 |> List.head |> stringToCharList
        let repr7 = signals |> ofLen 3 |> List.head |> stringToCharList
        let repr235 = signals |> ofLen 5 |> List.map stringToCharList
        let repr069 = signals |> ofLen 6 |> List.map stringToCharList
        let notIn r = List.filter (elemNotInList r)
        let andIn r = List.filter (elemInList r)
        let andInNo n r = List.filter (elemInNoOfLists n r)
        let andInAll r = List.filter (elemInAllLists r)
        let all = stringToCharList "abcdefg"
        let a = all |> notIn repr1 |> notIn repr4 |> andIn repr7 |> andInAll repr235 |> andInAll repr069 |> List.head
        let b = all |> notIn repr1 |> andIn repr4 |> notIn repr7 |> andInNo 1 repr235 |> andInAll repr069 |> List.head
        let c = all |> andIn repr1 |> andIn repr4 |> andIn repr7 |> andInNo 2 repr235 |> andInNo 2 repr069 |> List.head
        let d = all |> notIn repr1 |> andIn repr4 |> notIn repr7 |> andInAll repr235 |> andInNo 2 repr069 |> List.head
        let e = all |> notIn repr1 |> notIn repr4 |> notIn repr7 |> andInNo 1 repr235 |> andInNo 2 repr069 |> List.head
        let f = all |> andIn repr1 |> andIn repr4 |> andIn repr7 |> andInNo 2 repr235 |> andInAll repr069 |> List.head
        let g = all |> notIn repr1 |> notIn repr4 |> notIn repr7 |> andInAll repr235 |> andInAll repr069 |> List.head
        let rev = Map.empty |> Map.add a 'a' |> Map.add b 'b' |> Map.add c 'c' |> Map.add d 'd' |> Map.add e 'e' |> Map.add f 'f' |> Map.add g 'g'
        let nums = Map.empty |> Map.add "abcefg" 0 |> Map.add "cf" 1 |> Map.add "acdeg" 2 |> Map.add "acdfg" 3 |> Map.add "bcdf" 4 |> Map.add "abdfg" 5
                   |> Map.add "abdefg" 6 |> Map.add "acf" 7 |> Map.add "abcdefg" 8 |> Map.add "abcdfg" 9
        let convertOutput o = o |> stringToCharList |> List.map (fun a -> Map.find a rev) |> charListToString
        outputs |> List.map convertOutput |> List.map withCharsInOrder |> List.map (fun s -> Map.find s nums) |> List.map string |> List.reduce (+) |> int

    let Part2 () = loadInput |> List.map (fun (s, o) -> calc s o) |> List.sum
