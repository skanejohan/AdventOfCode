namespace AdventOfCode

open System

module GridN =

    // combine [7; 8; 9] [1; 2; 3; 4] -> [ [7; 1]; [7; 2]; [7; 3]; [7; 4]; 
    //                                     [8; 1]; [8; 2]; [8; 3]; [8; 4]; 
    //                                     [9; 1]; [9; 2]; [9; 3]; [9; 4] ]
    let rec combine list1 list2 = 
        match list1 with
        | h :: tl -> List.map (fun x -> h :: [x]) list2 @ combine tl list2
        | _       -> []

    // addToCombination [7; 8] [ [1; 2]; [3; 4] ] -> [ [7; 1; 2]; [7; 3; 4]; [8; 1; 2]; [8; 1; 4] ] 
    let rec addToCombination list lists = 
        match list with
        | h :: tl -> List.map (fun x -> h :: x) lists @ addToCombination tl lists
        | _       -> []

    // allCombinations [ [1; 2]; [3; 4]; [5; 6; 7] ] -> [ [1; 3; 5]; [1; 3; 6]; [1; 3; 7]; 
    //                                                    [1; 4; 5]; [1; 4; 6]; [1; 4; 7]; 
    //                                                    [2; 3; 5]; [2; 3; 6]; [2; 3; 7]; 
    //                                                    [2; 4; 5]; [2; 4; 6]; [2; 4; 7] ]
    let rec allCombinations (lists : 'a list list) =
        match lists with
        | [l1; l2] -> combine l1 l2
        | l1 :: ls -> addToCombination l1 (allCombinations ls)
        | _        -> []

    // neighborCoords [2; 5; 6] = [[1; 4; 5]; [2; 4; 5]; [3; 4; 5]; 
    //                             [1; 5; 5]; [2; 5; 5]; [3; 5; 5];
    //                             [1; 6; 5]; [2; 6; 5]; [3; 6; 5];
    //                             [1; 4; 6]; [2; 4; 6]; [3; 4; 6]; 
    //                             [1; 5; 6];            [3; 5; 6];
    //                             [1; 6; 6]; [2; 6; 6]; [3; 6; 6];
    //                             [1; 4; 7]; [2; 4; 7]; [3; 4; 7]; 
    //                             [1; 5; 7]; [2; 5; 7]; [3; 5; 7];
    //                             [1; 6; 7]; [2; 6; 7]; [3; 6; 7];
    let neighborCoords coords = List.map (fun c -> [c-1; c; c+1]) coords |> allCombinations |> List.filter (fun cs -> cs <> coords)
        
    type IntGrid =
        struct
            val MinCoords: int list
            val MaxCoords: int list
            val Alive : Set<int list>

            new (minCoords: int list, maxCoords: int list, alive : Set<int list>) = 
                { MinCoords = minCoords; MaxCoords = maxCoords; Alive = alive }

            static member empty dimensions = 
                let minCoords = List.init dimensions (fun _ -> Int32.MaxValue)
                let maxCoords = List.init dimensions (fun _ -> -Int32.MaxValue)
                new IntGrid(minCoords, maxCoords, Set.empty)

            static member add coords (grid : IntGrid) = 
                let minCoords = List.zip grid.MinCoords coords |> List.map (fun (a,b) -> min a b)
                let maxCoords = List.zip grid.MaxCoords coords |> List.map (fun (a,b) -> max a b)
                new IntGrid(minCoords, maxCoords, Set.add coords grid.Alive)

            static member remove coords (grid : IntGrid) = 
                new IntGrid(grid.MinCoords, grid.MaxCoords, Set.remove coords grid.Alive)
        end
