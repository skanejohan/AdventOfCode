namespace AdventOfCode2021

module Day17 =

    let step (x, y, dx, dy) = 
        let dx' = if dx > 0 then dx-1 else if dx < 0 then dx+1 else dx
        (x + dx, y + dy, dx', dy - 1)

    let run xMin xMax yMin yMax (dx, dy) =
        let rec doRun state positions = 
            let (x, y, dx, dy) = step state
            let newPositions = (x, y) :: positions
            if y < yMin then None
            else if x >= xMin && x <= xMax && y >= yMin && y <= yMax
                 then Some newPositions
                 else doRun (x, y, dx, dy) newPositions
        doRun (0, 0, dx, dy) []

    let Part1 () = 
        let (xMin, xMax, yMin, yMax) = (150, 193, -136, -86)
        [for x in 1..xMax+1 do for y in yMin..1000 do yield (x, y)]
        |> List.map (run xMin xMax yMin yMax) 
        |> List.choose id
        |> List.map (fun l -> List.map snd l) 
        |> List.map List.max 
        |> List.max


    let Part2 () =
        let (xMin, xMax, yMin, yMax) = (150, 193, -136, -86)
        [for x in 1..xMax+1 do for y in yMin..1000 do yield (x, y)]
        |> List.map (run xMin xMax yMin yMax) 
        |> List.choose id 
        |> List.length
