namespace Lib.Collections

open BitList

module BitLists =

    type BitLists = BitLists of BitList list with

        // fromCharLists :: char list list -> BitLists
        static member fromCharLists cls = List.map BitList.fromCharList cls |> BitLists

        // length :: BitLists -> int
        static member length (BitLists l) = List.length l 

        // head :: BitLists -> BitList
        static member head (BitLists l) = List.head l

        // get :: BitLists -> int list list
        static member get (BitLists ls) = List.map BitList.get ls

        // sum :: BitLists -> int list
        static member sum (BitLists ls) =
            let len = List.head ls |> BitList.length
            let f acc elem = List.map2 (+) acc elem
            let l = List.map BitList.get ls
            List.fold f (List.init len (fun _ -> 0)) l

        // byBitSet :: int -> BitLists -> BitLists
        // Return only the BitList objects that have the given bit set.
        static member byBitSet bitPos (BitLists ls) = 
            List.filter (fun (v : int list) -> v.[bitPos] = 1) (List.map BitList.get ls) |> 
            List.map BitList |>
            BitLists

        // byBitClear :: int -> BitLists -> BitLists
        // Return only the BitList objects that don't have the given bit set.
        static member byBitClear bitPos (BitLists ls) = 
            List.filter (fun (v : int list) -> v.[bitPos] <> 1) (List.map BitList.get ls) |> 
            List.map BitList |>
            BitLists




    //// charListsToBitLists :: char list list -> BitList list
    //let charListsToBitLists = List.map BitList.fromCharList

    //// fromBitLists :: BitList list -> int list list
    //let fromBitLists (ls : BitList list) = List.map BitList.get ls

    //// sumBitLists :: BitList list -> IntList
    //let sumBitLists (ls : BitList list) =
    //    let len = List.head ls |> BitList.length
    //    let f acc elem = List.map2 (+) acc elem
    //    let l = List.map BitList.get ls
    //    List.fold f (List.init len (fun _ -> 0)) l |> IntList

    //// bitListsFilteredByBitSet :: int -> BitList list -> BitList list
    //// Return only the BitList objects that have the given bit set.
    //let bitListsFilteredByBitSet bitPos ls = 
    //    List.filter (fun (v : int list) -> v.[bitPos] = 1) (List.map BitList.get ls) |> 
    //    List.map BitList

    //// bitListsFilteredByBitClear :: int -> BitList list -> BitList list
    //// Return only the BitList objects that don't have the given bit set.
    //let bitListsFilteredByBitClear bitPos ls = 
    //    List.filter (fun (v : int list) -> v.[bitPos] <> 1) (List.map BitList.get ls) |> 
    //    List.map BitList
