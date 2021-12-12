namespace AdventOfCode2015

open Lib.DataLoader
open Lib.Regex

module Day07 =

    type Expr = 
         | Or of Expr * Expr
         | And of Expr * Expr
         | LShift of Expr * int
         | RShift of Expr * int
         | Not of Expr
         | Id of Expr
         | Var of string
         | Val of uint16 

    let tryParseInt s = 
        try 
            s |> int |> Some
        with :? System.FormatException -> 
            None

    let parseLine line = 
        let varOrVal s = match tryParseInt s with
                         | Some i -> Val (uint16 i)
                         | None   -> Var s
        match line with
                         | Regex @"(.*) OR (.*) -> (.*)" [ in1; in2; out ]    -> (out, Or (varOrVal in1, varOrVal in2))
                         | Regex @"(.*) AND (.*) -> (.*)" [ in1; in2; out ]   -> (out, And (varOrVal in1, varOrVal in2))
                         | Regex @"(.*) LSHIFT (\d*) -> (.*)" [ in1; s; out ] -> (out, LShift (varOrVal in1, int s))
                         | Regex @"(.*) RSHIFT (\d*) -> (.*)" [ in1; s; out ] -> (out, RShift (varOrVal in1, int s))
                         | Regex @"NOT (.*) -> (.*)" [ in1; out ]             -> (out, Not (varOrVal in1))
                         | Regex @"(.*) -> (.*)" [ in1; out ]                 -> (out, Id (varOrVal in1))
                         | _                                                  -> failwith "invalid input"

    let getFunctions fileName = getDataAsStringList (Spec2015.withPath fileName) 
                                |> List.map parseLine
                                |> Map

    let evaluate expression functions = 
        let rec eval expr pc = 
            let result = 
                match Map.tryFind expr pc with
                      | Some v -> (v, pc)
                      | None   -> match expr with
                                  | Or (e1, e2)    -> let (v', pc') = eval e1 pc
                                                      let (v'', pc'') = (eval e2 pc')
                                                      let res = v' ||| v''
                                                      (res, Map.add expr res pc'')
                                  | And (e1, e2)   -> let (v', pc') = eval e1 pc
                                                      let (v'', pc'') = (eval e2 pc')
                                                      let res = v' &&& v''
                                                      (res, Map.add expr res pc'')
                                  | LShift (e1, i) -> let (v', pc') = eval e1 pc
                                                      let res = v' <<< i
                                                      (res, Map.add expr res pc')
                                  | RShift (e1, i) -> let (v', pc') = eval e1 pc
                                                      let res = v' >>> i
                                                      (res, Map.add expr res pc')
                                  | Not e1         -> let (v', pc') = eval e1 pc
                                                      let res = ~~~v'
                                                      (res, Map.add expr res pc')
                                  | Id e1          -> let (v', pc') = eval e1 pc
                                                      let res = v'
                                                      (res, Map.add expr res pc')
                                  | Var s          -> let (v', pc') = eval (Map.find s functions) pc
                                                      let res = v'
                                                      (res, Map.add expr res pc')
                                  | Val i          -> (i, pc)
            //printfn "Evaluation %A = %A" expr result
            result

        eval expression Map.empty

    let Part1 () = 
        let functions = getFunctions "Day07Input.txt"
        let (result, data) = evaluate (Map.find "a" functions) functions
        result
    
    let Part2 () = 0
