namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open System

module Day04 =

    let rec private extractPassports data current =
        match data with 
        | "" :: xs -> current :: extractPassports xs ""
        | x :: xs -> extractPassports xs (current + " " + x)
        | _ -> [current]

    let private passportOk1 (p : string) = p.Contains "byr" && p.Contains "iyr" && p.Contains "eyr" && p.Contains "hgt" && 
                                           p.Contains "hcl" && p.Contains "ecl" && p.Contains "pid" 

    let Part1 _ = extractPassports (GetDataAsStringList "Day04Input.txt") "" |> List.filter passportOk1 |> List.length

    let private splitPassport (p : string) = p.Split([|" "|], StringSplitOptions.None) |> Array.toList

    let private splitField (f: string) = f.Split([|":"|], StringSplitOptions.None)

    let rec private getValue field passport = 
        match passport with
        | f :: fs -> let fv = splitField f
                     if fv.[0] = field then fv.[1] else getValue field fs
        | _       -> "0"

    let private byrOk p =
        let byr = getValue "byr" p |> int
        byr >= 1920 && byr <= 2002
    
    let private iyrOk p =
        let iyr = getValue "iyr" p |> int
        iyr >= 2010 && iyr <= 2020

    let private eyrOk p =
        let eyr = getValue "eyr" p |> int
        eyr >= 2020 && eyr <= 2030

    let private hgtOk p =
        let hgt = getValue "hgt" p
        match hgt with
        | Regex @"([0-9]+)([a-z]+)" [ value; unit ] -> if unit = "cm" && (int value) >= 150 && (int value) <= 193
                                                       then true
                                                       else unit = "in" && (int value) >= 59 && (int value) <= 76                                                            
        | _                                         -> false

    let private hclOk p =
        let hcl = getValue "hcl" p
        match hcl with
        | Regex @"[#][a-f0-9]{6}" [ ] -> true
        | _                           -> false

    let private eclOk p = 
        let ecl = getValue "ecl" p
        ecl = "amb" || ecl = "blu" || ecl = "brn" || ecl = "gry" || ecl = "grn" || ecl = "hzl" || ecl = "oth"

    let private pidOk p =
        let pid = getValue "pid" p
        match pid with
        | Regex @"^[0-9]{9}$" [ ] -> true
        | _                       -> false

    let private passportOk2 (p : string) = 
        passportOk1 p &&
        byrOk (splitPassport p) &&
        iyrOk (splitPassport p) &&
        eyrOk (splitPassport p) &&
        hgtOk (splitPassport p) &&
        hclOk (splitPassport p) &&
        eclOk (splitPassport p) &&
        pidOk (splitPassport p)

    let Part2 _ = extractPassports (GetDataAsStringList "Day04Input.txt") "" |> List.filter passportOk2 |> List.length
