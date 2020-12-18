namespace AdventOfCode

module ListUtils = 

    let rec flatten list =
        match list with
        | l :: ls -> l @ flatten ls
        | _       -> []
