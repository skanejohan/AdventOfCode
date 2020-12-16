namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day16 =

    let parseInput lines = 
        let rec parse rules tickets lines =
            let rulesRegExp = @"[a-z ]+\: ([0-9]+)[-]([0-9]+) or ([0-9]+)[-]([0-9]+)"
            match lines with
            | l :: ls -> match l with
                         | Regex rulesRegExp [ min1; max1; min2; max2 ] -> parse (List.Cons ((int min1, int max1), List.Cons ((int min2, int max2), rules))) tickets ls
                         | Regex @"(\d+)(,\d+)*" [ _; _ ]               -> let nums = l.Split "," |> Array.map int |> Array.toList
                                                                           parse rules (List.Cons (nums,tickets)) ls 
                         | _                                            -> parse rules tickets ls
            | _       -> let l = List.length tickets
                         let your = List.skip (l-1) tickets |> List.head
                         let nearby = List.take (l-1) tickets
                         (List.rev rules, your, List.rev nearby)
        parse [] [] lines

    let isValid rules n = List.exists (fun (min,max) -> min <= n && max >= n) rules

    let isInvalid rules n = not (isValid rules n)

    let Part1 _ = 
        let (rules, your, nearby) = GetDataAsStringList "Day16Input.txt" |> parseInput
        flatten nearby |> List.filter (isInvalid rules) |> List.sum

    let Part2 _ = 
        let hasInvalid rules list = List.exists (isInvalid rules) list
        let hasNoInvalid rules list = not (hasInvalid rules list)
        let zipWithIndex xs = List.zip [0..List.length xs-1] xs
        let findMatch indexedRules nums = List.filter (fun (idx, rules) -> hasNoInvalid rules nums) indexedRules
        let select rulesPassed = 
            let rec check passedSoFar rules = 
                let passed = List.filter (fun (a,b) -> List.length b = 1) rules
                let notpassed = List.filter (fun (a,b) -> List.length b > 1) rules
                let rulesDone = List.map snd passed |> flatten
                let remainingRules = notpassed |> List.map (fun (a,b) -> (a, List.filter (fun b -> not (List.contains b rulesDone)) b))
                if List.length remainingRules = 0
                then passed @ passedSoFar
                else check (passed @ passedSoFar) remainingRules
            check [] rulesPassed |>
            List.rev |>
            List.map (fun (a,b) -> (a,List.head b))

        let (rules, your, nearby) = GetDataAsStringList "Day16Input.txt" |> parseInput
        let positionValues = nearby |> List.filter (hasNoInvalid rules) |> rotateLists
        let indexedRules = 
            List.zip rules [0..List.length rules-1] |> 
            List.map (fun (a,b) -> (a, b/2)) |> 
            List.groupBy (fun (a, b) -> b) |> 
            List.map (fun (a, b) -> (a, List.map fst b))
        let possibleRulesForAllPositions = positionValues |> 
                                           List.map (findMatch indexedRules) |> 
                                           zipWithIndex |>
                                           List.map (fun (a,ls) -> (a, List.map fst ls))

        // This gives 
        //  [(0, 19); (1, 7); (5, 17); (16, 9); (7, 16); (10, 8); (4, 11); (18, 10); (6, 14);
        //   (14, 3); (11, 2); (8, 0); (19, 4); (2, 5); (17, 1); (3, 12); (15, 15); (9, 18);
        //   (12, 13); (13, 6)]
        //
        // The departure fields are the first 6 rules (0-5), i.e. the positions that match those -
        //  14, 11, 8, 19, 2, 17

        (int64 your.[14]) * (int64 your.[11]) * (int64 your.[8]) * (int64 your.[19]) * (int64 your.[2]) * (int64 your.[17])














        //let passed = List.filter (fun (a,b) -> List.length b = 1) x
        //let notpassed = List.filter (fun (a,b) -> List.length b > 1) x
        //printfn "position values: %A" positionValues
        //printfn "indexed rules:   %A" indexedRules
        //printfn "%s" "rules passed:"
        //passed |> List.map (printfn "  %A") |> ignore
        //printfn "%s" "rules not passed:"
        //notpassed |> List.map (printfn "  %A") |> ignore


















        //let x = positionValues [5; 3; 88; 6] 
        //let y = List.zip [1; 2; 3; 4] [5; 6; 7; 8]

        //printfn "%A" (rotateLists ok)


        //let rec combine list1 list2 = 
        //    match list1 with
        //    | x :: xs -> match list2 with 
        //                 | y :: ys -> (x @ y) :: combine xs ys
        //                 | _       -> []
        //    | _       -> []

        //let combine2 lists = List.fold (fun s a -> combine s a) (List.head lists) (List.tail lists)

        //let a = [5; 14; 9; 12] 
        //let b = [15; 1; 5; 1]
        //let c = [3; 9; 18; 7]

        //let input = [a; b; c]
        //let xx = List.map (List.map (fun a -> [a])) input
        ////let d = List.map (fun a -> [a]) a
        ////let e = List.map (fun a -> [a]) b
        ////let f = List.map (fun a -> [a]) c

        //let result = combine2 xx //[d; e; f]
        
        ////printfn "%A" result

        //let x = [a; b; c]
        //let y = List.map (List.map (fun a -> [a])) ok

        //// [[5; 14; 9]; [15; 1; 5]; [3; 9; 18]] -> [[5; 15; 3]; [14; 1; 9]; [9; 5; 18]]

        ////let z = List.fold (fun s a -> combine s a) d [e]

        
        ////let bb = ok |> List.map (fun a -> [a])

        ////printfn "%A" (combine2 y)

        //let f = flatten nearby
        //let invalidNearby = flatten nearby |> List.filter (isInvalid rules)
        //invalidNearby |> List.sum

        //// [5, 3, 88, 6] -> [0->5; 1->3; 2->88; 3->6]
        //let positionValues list = List.zip list [0..list.Length-1] |> List.fold (fun m (value,idx) -> Map.add idx value m) Map.empty 

        // [0->5; 1->3; 2->88; 3->6] [0->7; 1->11; 2->88; 3->6]

        //let ticketPositionValues tickets = 
