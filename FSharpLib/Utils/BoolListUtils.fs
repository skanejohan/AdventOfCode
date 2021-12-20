namespace Lib.Utils

module BoolListUtils =

    // boolsToInt [true, false] = 2, boolsToInt [true; false; true; false] = 10 etc.
    let boolsToInt bools = // TODO general
        let rec b2i bs ack = 
            match bs with
            | true :: bss  -> b2i bss (1 + (ack <<< 1))
            | false :: bss -> b2i bss (ack <<< 1)
            | _            -> ack
        b2i bools 0

