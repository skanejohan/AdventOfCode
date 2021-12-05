namespace Lib.Extensions

module ListExtensions =

    type Microsoft.FSharp.Collections.List<'a> with

        static member flatten l =
            let rec f l' =
                match l' with
                | l'' :: ls -> l'' @ f ls
                | _         -> []
            f l
