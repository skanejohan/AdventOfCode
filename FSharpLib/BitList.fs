namespace Lib

open IntList

module BitList =

    type BitList = BitList of int list

    // initBitList :: int -> BitList
    let initBitList len = BitList (List.init len (fun _ -> 0))

    // charToBit :: char -> int
    let charToBit c = if c = '0' then 0 else 1

    // charListToBitList :: char list -> BitList
    let charListToBitList cs = BitList (List.map charToBit cs)

    // charListsToBitLists :: char list -> BitList
    let charListsToBitLists = List.map charListToBitList

    // fromBitList :: BitList -> int list
    let fromBitList (BitList l) = l

    // fromBitLists :: BitList list -> int list list
    let fromBitLists = List.map fromBitList

    // bitListToIntList :: BitList -> IntList
    let bitListToIntList (BitList l) = IntList l 

    // bitListLength :: BitList -> int
    let bitListLength (BitList l) = List.length l 

    // bitListFold :: (int list -> int -> int list) -> int list -> BitList -> BitList
    let bitListFold folder state (BitList l) = BitList (List.fold folder state l)

    // sumBitLists :: BitList list -> IntList
    let sumBitLists (ls : BitList list) =
        let len = List.head ls |> bitListLength
        let f acc elem = List.map2 (+) acc elem
        let l = List.map fromBitList ls
        List.fold f (List.init len (fun _ -> 0)) l |> IntList

    // bitListInvert :: BitList -> BitList
    let bitListInvert (BitList l) = List.map (fun b -> 1 - b) l |> BitList

    // bitListToInt64 :: BitList -> int64
    let bitListToInt64 (BitList l) = 
        let rec raise2 n = 
            match n with
            | 0L -> 1L
            | x  -> 2L * raise2 (n-1L)
        List.rev l |>
        List.zip (List.map int64 [0..List.length l - 1]) |>
        List.map (fun (a,b) -> (raise2 a) * int64 b) |>
        List.sum

    // bitListsFilteredByBitSet :: int -> BitList list -> BitList list
    // Return only the BitList objects that have the given bit set.
    let bitListsFilteredByBitSet bitPos ls = 
        List.filter (fun (v : int list) -> v.[bitPos] = 1) (List.map fromBitList ls) |> 
        List.map BitList

    // bitListsFilteredByBitClear :: int -> BitList list -> BitList list
    // Return only the BitList objects that don't have the given bit set.
    let bitListsFilteredByBitClear bitPos ls = 
        List.filter (fun (v : int list) -> v.[bitPos] <> 1) (List.map fromBitList ls) |> 
        List.map BitList
