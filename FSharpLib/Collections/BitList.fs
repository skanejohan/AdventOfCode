namespace Lib.Collections

module BitList =

    type BitList = BitList of int list with

        // init :: int -> BitList
        static member init len = BitList (List.init len (fun _ -> 0))

        // fromCharList :: :: char list -> BitList
        static member fromCharList cs = BitList (List.map (fun c -> if c = '0' then 0 else 1) cs)

        // length :: BitList -> int
        static member length (BitList l) = List.length l 

        // invert :: BitList -> BitList
        static member invert (BitList l) = List.map (fun b -> 1 - b) l |> BitList

        // get :: BitList -> int list
        static member get (BitList l) = l

        // toInt64 :: BitList -> int64
        static member toInt64 (BitList l) = 
            let rec raise2 n = 
                match n with
                | 0L -> 1L
                | x  -> 2L * raise2 (n-1L)
            List.rev l |>
            List.zip (List.map int64 [0..List.length l - 1]) |>
            List.map (fun (a,b) -> (raise2 a) * int64 b) |>
            List.sum
