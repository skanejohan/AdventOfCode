namespace Lib.Extensions

module ListExtensions =

    type Microsoft.FSharp.Collections.List<'a> with

        static member flatten l =
            let rec f l' =
                match l' with
                | l'' :: ls -> l'' @ f ls
                | _         -> []
            f l

        static member split length (xs: List<'a>) =
                let rec loop xs =
                    [
                        yield List.truncate length xs
                        match List.length xs <= length with
                        | false -> yield! loop (List.skip length xs)
                        | true -> ()
                    ]
                loop xs

        // [[5; 14; 9; 12]; [15; 1; 5; 1]; [3; 9; 18; 7]] -> [[5; 15; 3]; [14; 1; 9]; [9; 5; 18]; [12; 1; 7]]
        static member transpose ls = 
                let rec combine l1 l2 = 
                    match l1 with
                    | x :: xs -> match l2 with 
                                 | y :: ys -> (x @ y) :: combine xs ys
                                 | _       -> []
                    | _       -> []
                let comb2 ls = List.fold (fun s a -> combine s a) (List.head ls) (List.tail ls)
                comb2 (List.map (List.map (fun a -> [a])) ls)

