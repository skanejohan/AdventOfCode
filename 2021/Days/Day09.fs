namespace AdventOfCode2021

open Lib.DataLoader

module Day09 =
    
    let getDataWidthHeightAndGetFunction fileName = 
        let data = getDataAsArray2D (Spec2021.withPath fileName) |> Array2D.map (fun c -> int c - int '0')
        (data, Array2D.length1 data, Array2D.length2 data, Array2D.get data)

    let getLowPoints fileName = 
        let (data, w, h, get) = getDataWidthHeightAndGetFunction fileName
        let lowerThanUp x y = if y = 0 then true else get x y < get x (y-1)
        let lowerThanRight x y = if x = w-1 then true else get x y < get (x+1) y
        let lowerThanDown x y = if y = h-1 then true else get x y < get x (y+1)
        let lowerThanLeft x y = if x = 0 then true else get x y < get (x-1) y
        let low x y c = lowerThanUp x y && lowerThanRight x y && lowerThanDown x y && lowerThanLeft x y
        Array2D.mapi (fun x y c -> if low x y c then c+1 else 0) data

    let findLowPointCoords lowPoints = lowPoints 
                                       |> Array2D.mapi (fun x y c -> (x, y, c)) 
                                       |> Seq.cast<(int * int * int)> 
                                       |> Seq.filter (fun (x, y, c) -> c <> 0)
                                       |> Seq.map (fun (x, y, c) -> (x, y))
                                       |> Seq.toList

    let Part1 () = getLowPoints "Day09Input.txt" |> Seq.cast<int> |> Seq.sum

    let Part2 () = 
        let (data, w, h, get) = getDataWidthHeightAndGetFunction "Day09Input.txt"
        let lowPoints = getLowPoints "Day09Input.txt" |> findLowPointCoords
        let calcBasin (xc, yc) =
            let rec calc x y set = if y = -1 || y = h || x = -1 || x = w || get x y = 9||  Set.contains (x, y) set then set else (Set.add (x, y) set) |> calc (x-1) y |> calc x (y-1) |> calc (x+1) y |> calc x (y+1)
            calc xc yc Set.empty
        let basinSizes = lowPoints |> List.map calcBasin |> List.map Set.count |> List.sort |> List.rev |> List.map int64|> List.toArray
        basinSizes.[0] * basinSizes.[1] * basinSizes.[2]