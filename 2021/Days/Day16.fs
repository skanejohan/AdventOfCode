namespace AdventOfCode2021

open Lib.Utils.HexUtils
open Lib.DataLoader

module Day16 =

    let byteToBits (byteVal : byte) = 
        let rec b2b b n = match n with
                          | 0 -> []
                          | _ -> let posVal = byte (pown 2 (n-1))
                                 let x = if b >= posVal then 1uy else 0uy
                                 x :: b2b (b - x * posVal) (n-1)
        b2b byteVal 8

    let bitsToInt bits = 
        let rec b2i bs acc = match bs with 
                             | x :: xs -> b2i xs ((bigint (uint64 x) + (bigint 2UL) * acc))
                             | _ -> acc
        b2i bits (bigint 0UL)
        
    type OpType = Sum | Prod | Min | Max | GT | LT | EQ

    type Packet = | Literal of int * bigint | Operator of int * OpType * Packet list

    let rec versionSum packet = 
        match packet with
        | Operator (v, op, pss) -> let subSums = List.map versionSum pss
                                   v + List.sum subSums
        | Literal (v, _) -> v

    let rec eval packet = 
        match packet with
        | Literal (_, v) -> v
        | Operator (v, OpType.Sum, pss)  -> pss |> List.map eval |> List.sum
        | Operator (v, OpType.Prod, pss) -> pss |> List.map eval |> List.fold (*) (bigint 1)
        | Operator (v, OpType.Min, pss)  -> pss |> List.map eval |> List.min
        | Operator (v, OpType.Max, pss)  -> pss |> List.map eval |> List.max
        | Operator (v, OpType.GT, pss)   -> pss |> List.map eval |> fun l -> if l.[0] > l.[1] then (bigint 1UL) else (bigint 0UL)
        | Operator (v, OpType.LT, pss)   -> pss |> List.map eval |> fun l -> if l.[0] < l.[1] then (bigint 1UL) else (bigint 0UL)
        | Operator (v, OpType.EQ, pss)   -> pss |> List.map eval |> fun l -> if l.[0] = l.[1] then (bigint 1UL) else (bigint 0UL)

    // State = remaining bits and number of bits parsed so far
    type State = State of int list * int with
        static member bits s = match s with State (bits, _) -> bits
        static member parsed s = match s with State (_, p) -> p

    let takeBits n s = 
        let bits = State.bits s
        let result = List.take n bits
        let newS = State (List.skip n bits, State.parsed s + n)
        (result, newS)

    let takeInt n s = 
        let (bs, s) = takeBits n s
        (bitsToInt bs, s)

    let takeBit s = 
        let bits = State.bits s
        let result = List.head bits
        let newS = State (List.skip 1 bits, State.parsed s + 1)
        (result, newS)

    let parseNumber s =
        let rec parseN s1 =
            let (n, s2) = takeBits 5 s1
            let thisN = List.tail n
            if (List.head n = 0) 
            then (thisN, s2)
            else let (nums, s3) = parseN s2
                 (List.concat [ thisN; nums ], s3)
        let (num, s1) = parseN s
        (bitsToInt num, s1)

    let rec parsePacket s =
        let (biVer, s2) = takeInt 3 s
        let (biTyp, s3) = takeInt 3 s2
        let ver = int biVer
        let typ = int biTyp
        let (p, s4) = if typ = 4 
                      then let (num, s4) = parseNumber s3
                           (Literal (int ver, num), s4)
                      else let (lt, s4) = takeBit s3
                           let (packets, s5) = if lt = 0 
                                               then let (len, s5) = takeInt 15 s4
                                                    parsePacketsUntil (State.parsed s5 + (int len)) s5
                                               else let (num, s5) = takeInt 11 s4
                                                    parsePacketsByCount (int num) s5
                           match typ with
                           | 0 -> (Operator (ver, OpType.Sum, packets), s5)
                           | 1 -> (Operator (ver, OpType.Prod, packets), s5)
                           | 2 -> (Operator (ver, OpType.Min, packets), s5)
                           | 3 -> (Operator (ver, OpType.Max, packets), s5)
                           | 5 -> (Operator (ver, OpType.GT, packets), s5)
                           | 6 -> (Operator (ver, OpType.LT, packets), s5)
                           | _ -> (Operator (ver, OpType.EQ, packets), s5)
        (p, s4)

    and parsePacketsByCount n s =
        let (packet, s2) = (parsePacket s)
        if n = 1 
        then ([packet], s2) 
        else let (packets, s3) = parsePacketsByCount (n-1) s2
             (packet :: packets, s3)
        
    and parsePacketsUntil n s =
        let (packet, s2) = (parsePacket s)
        if State.parsed s2 >= n 
        then ([packet], s2) 
        else let (packets, s3) = parsePacketsUntil n s2
             (packet :: packets, s3)

    let toBitList s = s |> hexStringToByteArray |> Array.map byteToBits |> Array.toList |> List.concat |> List.map int

    let getPacket s = let (result, _) = parsePacket (State (s |> toBitList, 0))
                      result

    let Part1 () = Spec2021.withPath "Day16Input.txt" |> getDataAsString |> getPacket |> versionSum
    
    let Part2 () = Spec2021.withPath "Day16Input.txt" |> getDataAsString |> getPacket |> eval
