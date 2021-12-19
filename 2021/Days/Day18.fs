namespace AdventOfCode2021

open Lib.Library
open Lib.Utils.CharUtils

module Day18 =

    type SnailfishNumber = 
    | Num of int 
    | Pair of (SnailfishNumber * SnailfishNumber)

    let isSameObject = LanguagePrimitives.PhysicalEquality

    type State = State of (SnailfishNumber option * SnailfishNumber option * SnailfishNumber option * SnailfishNumber option) with 
        static member setLastNumBefore state n = match state with State (_, b, c, d) -> State (Some n, b, c, d)
        static member hasThisLastNumBefore state n = match state with State (a, _, _, _) -> a.IsSome && isSameObject a.Value n
        static member setPair state p = match state with State (a, _, c, d) -> State (a, Some p, c, d)
        static member hasPair state = match state with State (_, b, _, _) -> b.IsSome
        static member getPairLeft state = match state with State (_, b, _, _) -> match b with 
                                                                                 | Some (Pair (Num a, _)) -> a
                                                                                 | _                      -> failwith "no pair"
        static member getPairRight state = match state with State (_, b, _, _) -> match b with 
                                                                                  | Some (Pair (_, Num b)) -> b
                                                                                  | _                      -> failwith "no pair"
        static member hasThisPair state p = match state with State (_, b, _, _) -> b.IsSome && isSameObject b.Value p
        static member setFirstNumAfter state n = match state with State (a, b, _, d) -> State (a, b, Some n, d)
        static member hasFirstNumAfter state = match state with State (_, _, c, _) -> c.IsSome
        static member hasThisFirstNumAfter state n = match state with State (_, _, c, _) -> c.IsSome && isSameObject c.Value n
        static member setNumberAboveNine state n = match state with State (a, b, c, d) -> State (a, b, c, Some n)
        static member hasNumberAboveNine state = match state with State (_, _, _, d) -> d.IsSome
        static member hasThisNumberAboveNine state n = match state with State (_, _, _, d) -> d.IsSome && isSameObject d.Value n

    let findExplodeOrSplitInfo snailfishNumber = 
        let canSetPair l s = l = 4 && not (State.hasPair s)
        let canSetNumberAboveNine s = not (State.hasPair s) && not (State.hasNumberAboveNine s)
        let rec find snum l s =
            match snum with
            | Pair (Num i1, Num i2) as p when canSetPair l s   -> State.setPair s p
            | Pair (sn1, sn2)                                  -> let s2 = find sn1 (l+1) s
                                                                  find sn2 (l+1) s2
            | Num i as n when i > 9 && canSetNumberAboveNine s -> State.setNumberAboveNine s n 
            | Num i as n                                       -> if not (State.hasPair s)
                                                                  then State.setLastNumBefore s n
                                                                  else if State.hasFirstNumAfter s
                                                                      then s
                                                                      else State.setFirstNumAfter s n
        find snailfishNumber 0 (State (None, None, None, None))

    let explodeOrSplit snailfishNumber =
        let rec eos snum s =
            match snum with
            | Pair _ as p when State.hasThisPair s p                                    -> Num 0
            | Num i as n when not (State.hasPair s) && State.hasThisNumberAboveNine s n -> let f = float i
                                                                                           let n1 = floor (f / 2.0) |> int
                                                                                           let n2 = ceil (f / 2.0) |> int
                                                                                           Pair (Num n1, Num n2)
            | Num i as n when State.hasPair s && State.hasThisLastNumBefore s n         -> Num (i + State.getPairLeft s)
            | Num i as n when State.hasPair s && State.hasThisFirstNumAfter s n         -> Num (i + State.getPairRight s)
            | Pair (a, b)                                                               -> Pair (eos a s, eos b s)
            | sn                                                                        -> sn
        let rec eosWhileNeeded snum s =
            let result = eos snum s
            if result = snum 
            then snum
            else eosWhileNeeded result (findExplodeOrSplitInfo result)
        eosWhileNeeded snailfishNumber (findExplodeOrSplitInfo snailfishNumber)
   
    let explodeOrSplitDebug snailfishNumber =
        let rec prettyPrint snailfishNumber = 
            match snailfishNumber with
            | Num i -> string i
            | Pair (a, b) -> "[" + prettyPrint a + "," + prettyPrint b + "]"
        let result = explodeOrSplit snailfishNumber
        printfn "%s" (prettyPrint result)
        result

    let add snailfishNumber1 snailfishNumber2 = Pair (snailfishNumber1, snailfishNumber2) |> explodeOrSplit

    let sum snailfishNumbers = List.fold add (List.head snailfishNumbers) (List.tail snailfishNumbers)

    let rec magnitude snailfishNumber =
        match snailfishNumber with
        | Pair (l, r) -> 3 * (magnitude l) + 2 * (magnitude r)
        | Num i       -> i

    let parseInput input = 
        let rec parse s stack = 
            match s with
            | c :: cs when charIsNum c -> parse cs (Num (charToNum c) :: stack)
            | c :: cs when c = ']' -> let n2 = stack |> List.head
                                      let n1 = stack |> List.tail |> List.head
                                      parse cs (Pair (n1, n2) :: (stack |> List.tail |> List.tail))
            | c :: cs              -> parse cs stack
            | _ -> stack
        parse (input |> Seq.toList) [] |> List.head

    let getData = Lib.DataLoader.getDataAsStringList (Spec2021.withPath "Day18Input.txt") |> List.map parseInput

    let Part1 () = getData |> sum |> magnitude

    let Part2 () = 
        let data = getData
        let allPairs = List.concat [ data |> pairs |> Seq.toList ; data |> List.rev |> pairs |> Seq.toList ]
        let allSums = allPairs |> List.map (fun (a, b) -> add a b)
        allSums |> List.map magnitude |> List.max
