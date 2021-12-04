namespace AdventOfCode2021

module IntList =

    type IntList = IntList of int list

    // initIntList :: int -> IntList
    let initIntList len = IntList (List.init len (fun _ -> 0))

    // fromIntList :: IntList -> int list
    let fromIntList (IntList l) = l
