namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day21 =

    let parseData data =
        let parseLine (line : string) = 
            let parts = line.Split '('
            let ingredients = parts.[0].Split ' ' |> Array.filter (fun s -> s <> "")
            let allergens = parts.[1].Substring(9, parts.[1].Length - 10).Split ',' |> Array.map (fun s -> s.Trim())
            (ingredients, allergens)
        let rec updatePossibleIngredients ingredients allergens map =
            match allergens with
            | a :: aa -> let newIs = match Map.tryFind a map with
                                     | Some is -> intersection [is; ingredients] |> Set.toList
                                     | None    -> ingredients
                         updatePossibleIngredients ingredients aa (Map.add a newIs map)
            | _       -> map
        let rec parse lines (is : Map<string,int>) (possIs : Map<string,string list>) =
            match lines with
            | l :: ls -> let (ii, aa) = parseLine l
                         let newIs = Seq.fold (fun s i -> Map.add i ((get i s 0) + 1) s) is ii
                         let newPossIs = updatePossibleIngredients (Seq.toList ii) (Seq.toList aa) possIs
                         parse ls newIs newPossIs
            | _       -> (is, possIs)
        parse data Map.empty Map.empty

    let ingredientsWithNoAllergens ingredients possibleIngredients =
        let ingredientsWithAllergens = possibleIngredients |> Map.toList |> List.map snd |> List.concat |> set
        let ingredientNames = ingredients |> Map.toList |> List.map fst 
        List.filter (fun i -> not (Set.contains i ingredientsWithAllergens)) ingredientNames

    let Part1 () =
        let (ingredients, possibleIngredients) = parseData (GetDataAsStringList "Day21Input.txt")
        let noAllergensIn = ingredientsWithNoAllergens ingredients possibleIngredients
        noAllergensIn |> List.map (fun i -> Map.find i ingredients) |> List.sum


    let buildCanonicalList allergensToIngredients = 
        let ingredientToRemove aToIsMap = // Map<string,[string]> -> (string,string)
            aToIsMap |> Map.toList |> List.find (fun (k,v) -> List.length v = 1) |> (fun ((i, aa)) -> (i, List.head aa))
        let removeIngredient aToIsMap allergen = // Map<string,[string]> -> string -> Map<string,[string]>
            aToIsMap |> Map.toList |> List.map (fun (k,v) -> (k,List.filter (fun x -> x <> allergen) v)) |> Map
        let removeAllergen possibleIngredients ingredient = // Map<string,[string]> -> string -> Map<string,[string]>
            possibleIngredients |> Map.remove ingredient
        let rec build aToIsMap canonical =
            match (Map.count aToIsMap) with
            | 0 -> canonical 
            | _ -> let (i, a) = ingredientToRemove aToIsMap
                   let withAllergenRemoved = removeIngredient aToIsMap a
                   let withIngredientRemoved =  removeAllergen withAllergenRemoved i
                   build withIngredientRemoved ((i, a) :: canonical)
        build allergensToIngredients []

    let Part2 () = 
        let (_, allergensToIngredients) = parseData (GetDataAsStringList "Day21Input.txt") 
        let canonicalList = buildCanonicalList allergensToIngredients
        List.sortBy (fun e -> fst e) canonicalList |> List.map snd |> String.concat ","
