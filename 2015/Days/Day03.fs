namespace AdventOfCode2015

open Library
open Microsoft.FSharp.Collections

module Day03 =
    
    let private newPos (x, y) c =
        match c with 
        | '^' -> (x, y-1)
        | '>' -> (x+1, y)
        | 'v' -> (x, y+1)
        | _   -> (x-1, y)

    let private update (visited : Map<(int * int), int>) pos = 
        let count = match visited.TryFind pos with
                    | Some a -> a + 1
                    | None   -> 1
        visited.Add (pos, count)

    let rec private processInput pos visited input =
        match input with
        | x :: xs -> processInput (newPos pos x) (update visited (newPos pos x)) xs
        | _       -> visited


    let Part1 _ = 
        let visited = processInput (0, 0) (Map.empty.Add ((0, 0), 1)) (GetDataAsCharList "Day03Input.txt")
        visited.Count

    let Part2 _ =
        let moves = GetDataAsCharList "Day03Input.txt" 
        let zipped = List.zip moves [1..moves.Length]
        let santasMoves = List.filter (fun (_,idx) -> idx % 2 = 1) zipped |> List.map (fun (m,_) -> m) 
        let roboSantasMoves = List.filter (fun (_,idx) -> idx % 2 = 0) zipped |> List.map (fun (m,_) -> m) 
        let santasVisited = processInput (0, 0) (Map.empty.Add ((0, 0), 1)) santasMoves
        let visited = processInput (0, 0) santasVisited roboSantasMoves
        visited.Count
